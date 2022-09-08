using Microsoft.Extensions.Configuration;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static async Task Main()
    {
        Console.Title = "Transaction";

        var endpointConfiguration = new EndpointConfiguration("Transaction");
        var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile($"appsettings.json");

        var config = configuration.Build();
        var databaseConnection = config.GetConnectionString("chanchy_dbConnection");
        var rabbitMQConnection = config.GetConnectionString("RabbitMQConnection");

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<ITransactionService, TransactionService>();
        containerSettings.ServiceCollection.AddScoped<ITransactionData, TransactionData>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
        containerSettings.ServiceCollection.AddDbContextFactory<TransactionContext>(opt => opt.UseSqlServer(databaseConnection));

        #region ReceiverConfiguration

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(DoTransfort), destination: "CustomerAccount");

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(databaseConnection);
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        var subscriptions = persistence.SubscriptionSettings();
        subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        dialect.Schema("dbo");
        #endregion

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "Messages.NSB.Commands");
        conventions.DefiningEventsAs(type => type.Namespace == "Messages.NSB.Events");

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}

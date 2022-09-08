

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "CustommerAccount";

        var endpointConfiguration = new EndpointConfiguration("CustomerAccount");

        var databaseConnection = "Server=DESKTOP-R5RADSP;Database=Transaction;Trusted_Connection=True;";
        var rabbitMQConnection = "host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<ICustomerAccountService, CustomerAccountService>();
        containerSettings.ServiceCollection.AddScoped<ICustomerAccountData, CustomerAccountData>();
        containerSettings.ServiceCollection.AddDbContextFactory<CustomerAccountContext>(opt => opt.UseSqlServer(databaseConnection));

        #region ReceiverConfiguration

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

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

        await endpointInstance.Stop();
    }
}

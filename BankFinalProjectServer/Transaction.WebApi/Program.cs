
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("SQLConnection");
var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQConnection");


#region back-end-use-nservicebus
builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("TransactionAPI");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();
    endpointConfiguration.SendOnly();
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(databaseConnection);
    });

    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("dbo");
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningEventsAs(type => type.Namespace == "Messages.NSB.Events");
    return endpointConfiguration;
});
#endregion
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

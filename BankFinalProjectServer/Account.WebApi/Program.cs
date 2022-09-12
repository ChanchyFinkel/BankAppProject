

var builder = WebApplication.CreateBuilder();
var databaseConnection = builder.Configuration.GetConnectionString("SQLConnection");



#region NSB configuration
var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQConnection");
var NSBConnection = builder.Configuration.GetConnectionString("NSBConnection");

builder.Host.UseNServiceBus(hostBuilderContext=>
{
var endpointConfiguration = new EndpointConfiguration("CustomerAccount");


endpointConfiguration.EnableInstallers();
endpointConfiguration.EnableOutbox();


var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(NSBConnection);
    });
var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();


var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
transport.ConnectionString(rabbitMQConnection);
transport.UseConventionalRoutingTopology(QueueType.Quorum);

//var subscriptions = persistence.SubscriptionSettings();
//subscriptions.CacheFor(TimeSpan.FromMinutes(1));
//dialect.Schema("dbo");

var conventions = endpointConfiguration.Conventions();
conventions.DefiningCommandsAs(type => type.Namespace == "Messages.NSB.Commands");
conventions.DefiningEventsAs(type => type.Namespace == "Messages.NSB.Events");


    return endpointConfiguration;
});

#endregion

//var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

//Console.WriteLine("waiting for messages...");
//Console.ReadLine();

//await endpointInstance.Stop();


builder.Services.AddAuthentication(x =>
{
x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
x.TokenValidationParameters = new TokenValidationParameters
{
ValidateIssuerSigningKey = true,
IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
ValidateIssuer = false,
ValidateAudience = false
};
});

#region Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString= builder.Configuration.GetConnectionString("chanchy_dbConnection");
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthData, AuthData>();
builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();
builder.Services.AddScoped<ICustomerAccountData, CustomerAccountData>();
builder.Services.AddScoped<IOperationsHistoryData, OperationsHistoryData>();
builder.Services.AddScoped<IOperationsHistoryService, OperationsHistoryService>();
builder.Services.AddDbContextFactory<CustomerAccountContext>(opt => opt.UseSqlServer(databaseConnection));
builder.Services.AddAutoMapper(typeof(Program));
#endregion
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankApp", Version = "v1" });
    // To Enable authorization using Swagger (JWT)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHandlerExecptionMiddleware();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
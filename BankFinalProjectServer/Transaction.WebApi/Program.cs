var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("SQLConnection");

#region NSB configuration
var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQConnection");

builder.Host.UseNServiceBus(hostBuilderContext =>
{
    //Console.Title = "Transaction";

    var endpointConfiguration = new EndpointConfiguration("Transaction");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(databaseConnection);
        });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("nsb");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    var routing = transport.Routing();
    routing.RouteToEndpoint(typeof(DoTransfort), "CustomerAccount");

    //var subscriptions = persistence.SubscriptionSettings();
    //subscriptions.CacheFor(TimeSpan.FromMinutes(1));

    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningCommandsAs(type => type.Namespace == "Messages.NSB.Commands");
    conventions.DefiningEventsAs(type => type.Namespace == "Messages.NSB.Events");

    return endpointConfiguration;
});

#endregion


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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionData, TransactionData>();
builder.Services.AddDbContextFactory<TransactionContext>(opt => opt.UseSqlServer(databaseConnection));
builder.Services.AddAutoMapper(typeof(Program));
#endregion
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transaction", Version = "v1" });
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
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

app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();







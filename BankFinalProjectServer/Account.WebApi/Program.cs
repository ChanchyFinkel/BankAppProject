var builder = WebApplication.CreateBuilder(args);

var databaseConnection = builder.Configuration.GetConnectionString("SQLConnection");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString= builder.Configuration.GetConnectionString("chanchy_dbConnection");
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthData, AuthData>();
builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();
builder.Services.AddScoped<ICustomerAccountData, CustomerAccountData>();
builder.Services.AddDbContextFactory<CustomerAccountContext>(opt => opt.UseSqlServer(databaseConnection));
builder.Services.AddAutoMapper(typeof(Program));

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

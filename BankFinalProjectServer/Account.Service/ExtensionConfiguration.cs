namespace Account.Service;
public static class ExtensionConfiguration
{
    public static void ExtensionsDI(this IServiceCollection service)
    {
        service.AddScoped<IAuthData,AuthData>();
        service.AddScoped<IAccountData, AccountData>();
        service.AddScoped<IOperationsHistoryData, OperationsHistoryData>();
        service.AddScoped<IEmailVerificationData, EmailVerificationData>();

    }
    public static void ExtensionContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContextFactory<AccountContext>(opt => opt.UseSqlServer(connectionString));
    }
}
namespace Account.Service;
public static class ExtensionConfiguration
{
    public static void ExtensionsDI(this IServiceCollection service)
    {
        service.AddSingleton<IAuthData,AuthData>();
        service.AddSingleton<IAccountData, AccountData>();
        service.AddSingleton<IOperationsHistoryData, OperationsHistoryData>();
        service.AddSingleton<IEmailVerificationData, EmailVerificationData>();

    }
    public static void ExtensionContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContextFactory<AccountContext>(opt => opt.UseSqlServer(connectionString));
    }
}
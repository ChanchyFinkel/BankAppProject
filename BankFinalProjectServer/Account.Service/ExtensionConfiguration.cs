namespace Account.Service;
public static class ExtensionConfiguration
{
    public static void ExtensionsDI(this IServiceCollection service)
    {
        service.AddScoped<IAccountData, AccountData>();
    }
    public static void ExtensionContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContextFactory<AccountContext>(opt => opt.UseSqlServer(connectionString));
    }
}
namespace Transaction.Service;
public static class ExtensionConfiguration
{
    public static void ExtensionsDI(this IServiceCollection service)
    {
        service.AddScoped<ITransactionData, TransactionData>();
    }
    public static void ExtensionContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContextFactory<TransactionContext>(opt => opt.UseSqlServer(connectionString));
    }
}
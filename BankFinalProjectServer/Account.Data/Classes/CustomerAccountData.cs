namespace CustomerAccount.Data.Classes;
public class CustomerAccountData : ICustomerAccountData
{
    private readonly IDbContextFactory<CustomerAccountContext> _factory;
    public CustomerAccountData(IDbContextFactory<CustomerAccountContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    public async Task<bool> CreateAccount(Account account)
    {
        try
        {
            using var context = _factory.CreateDbContext();
            await context.Account.AddAsync(account);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task CreateCustomer(Customer customer)
    {
        using var context = _factory.CreateDbContext();
        await context.Customer.AddAsync(customer);
    }
    public async Task<Account> GetAccountInfo(int accountID)
    {
        using var context = _factory.CreateDbContext();
        return await context.Account.Where(account => account.ID==accountID).Include(account => account.Customer).FirstAsync();
    }
    public async Task<bool> ExistsCustomerName(string email)
    {
        using var context = _factory.CreateDbContext();
        return await context.Customer.AnyAsync(c=>c.Email.Equals(email));
    }
}

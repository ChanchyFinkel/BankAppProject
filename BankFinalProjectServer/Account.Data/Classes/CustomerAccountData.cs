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

    }
    public async Task<bool> CreateCustomer(Customer customer)
    {

    }
    public async Task<Account> GetAccountInfo(string customerID)
    {
        using var context = _factory.CreateDbContext();
        return await context.Account.Include(account=>account.Customer).FirstOrDefaultAsync(account => account.CustomerID.Equals(customerID));
    }
    public async Task<bool> ExistsAccount(string email)
    {

    }
}

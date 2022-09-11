namespace CustomerAccount.Data.Classes;
public class CustomerAccountData : ICustomerAccountData
{
    private readonly IDbContextFactory<CustomerAccountContext> _factory;
    public CustomerAccountData(IDbContextFactory<CustomerAccountContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }
    public async Task CreateCustomer(Customer customer)
    {
        using var context = _factory.CreateDbContext();
        await context.Customer.AddAsync(customer);
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
    public async Task<Account> GetAccountInfo(int accountID)
    {
        using var context = _factory.CreateDbContext();
        return await context.Account.Where(account => account.ID == accountID).Include(account => account.Customer).FirstAsync();
    }
    public async Task<Account> GetAccountHolderInfo(int accountNumber)
    {
        using var context = _factory.CreateDbContext();
        return await context.Account.Where(account => account.ID == accountNumber).Include(account => account.Customer).FirstAsync();
    }
    public async Task<bool> ExistsAccountEmail(string email)
    {
        using var context = _factory.CreateDbContext();
        return await context.Customer.AnyAsync(c => c.Email.Equals(email));
    }

    public async Task<bool> ExistsAccountId(int accountID)
    {
        using var context = _factory.CreateDbContext();
        return await context.Account.AnyAsync(a => a.ID == accountID);
    }

    public async Task<int> GetAccountBalance(int accountID)
    {
        using var context = _factory.CreateDbContext();
        Account account = await context.Account.FirstAsync(a => a.ID == accountID);
        return account.Balance;
    }

    public async Task<bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID,  int ammount)
    {
        try
        {
            using var context = _factory.CreateDbContext();
            Account receiverAccount = await context.Account.FirstAsync(a => a.ID == recieverAccountID);
            receiverAccount.Balance += ammount;
            Account SenderAccount = await context.Account.FirstAsync(a => a.ID == senderAccountID);
            SenderAccount.Balance -= ammount;
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }

    }
}

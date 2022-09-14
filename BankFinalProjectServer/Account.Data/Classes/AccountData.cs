namespace Account.Data.Classes;
public class AccountData : IAccountData
{
    private readonly IDbContextFactory<AccountContext> _factory;
    public AccountData(IDbContextFactory<AccountContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }
    public async Task<bool> CreateAccount(Entities.Account account,Customer customer,int verificationCode)
    {
        try
        {
            using var context = _factory.CreateDbContext();
            EmailVerification emailVerification = await context.EmailVerification.FirstAsync(e => e.Email.Equals(customer.Email));
            if (emailVerification == null || emailVerification.ExpirationTime < DateTime.UtcNow || emailVerification.VerificationCode != verificationCode)
                return false;
            await context.Customer.AddAsync(customer);
            await context.Account.AddAsync(account);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<Entities.Account> GetAccountInfo(int accountID)
    {
        using var context = _factory.CreateDbContext();
        return await context.Account.Where(account => account.ID == accountID).Include(account => account.Customer).FirstAsync();
    }
    public async Task<Entities.Account> GetAccountHolderInfo(int accountNumber)
    {
        using var context = _factory.CreateDbContext();
        Entities.Account account = await context.Account.Where(account => account.ID == accountNumber).Include(account => account.Customer).FirstAsync();
        return account;
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
        Entities.Account account = await context.Account.FirstAsync(a => a.ID == accountID);
        return account.Balance;
    }
    public async Task<string> UpdateBalancesAndAddOperationsHistories(OperationsHistory senderOperation,OperationsHistory receiverOperation)
    {
        try
        {
            using var context = _factory.CreateDbContext();
            Entities.Account receiverAccount = await context.Account.FirstOrDefaultAsync(a => a.ID == receiverOperation.AccountID);
            if (receiverAccount == null)
                return "Receiver account ID is invalid!";
            Entities.Account senderAccount = await context.Account.FirstOrDefaultAsync(a => a.ID == senderOperation.AccountID);
            if (senderAccount == null)
                return "Sender account ID is invalid!";
            if (senderAccount.Balance < senderOperation.TransactionAmount)
                return "You don't have enough balance!";
            receiverAccount.Balance += receiverOperation.TransactionAmount;
            senderAccount.Balance -= senderOperation.TransactionAmount;
            receiverOperation.Balance = receiverAccount.Balance;
            senderOperation.Balance = senderAccount.Balance;
            await context.OperationsHistory.AddAsync(receiverOperation);
            await context.OperationsHistory.AddAsync(senderOperation);
            await context.SaveChangesAsync();
            return "";
        }
        catch
        {
            return "Updating the data in database - failed";
        }
    }
}

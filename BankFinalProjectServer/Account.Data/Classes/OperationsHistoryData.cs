namespace Account.Data.Classes;
public class OperationsHistoryData : IOperationsHistoryData
{
    private readonly IDbContextFactory<AccountContext> _factory;
    public OperationsHistoryData(IDbContextFactory<AccountContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }
    public async Task<bool> AddOperation(OperationsHistory operation)
    {
        try
        {
            using var context = _factory.CreateDbContext();
            await context.OperationsHistory.AddAsync(operation);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<List<OperationsHistory>> GetOperationsHistories(int accountID)
    {
        using var context = _factory.CreateDbContext();
        List<OperationsHistory> result = await context.OperationsHistory.Where(operation=>operation.AccountID==accountID).OrderByDescending(o => o.OperationTime).ToListAsync();
        return result;
    }
    public async Task<int> GetOperationAccountID(int transactionID,int accountID)
    {
        var context = _factory.CreateDbContext();
        var res = await context.OperationsHistory.FirstAsync(o => o.TransactionID == transactionID && o.AccountID != accountID);
        return res.AccountID;
    }
}
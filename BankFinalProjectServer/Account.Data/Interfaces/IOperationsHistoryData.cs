namespace Account.Data.Interfaces
{
    public interface IOperationsHistoryData
    {
        Task<List<OperationsHistory>> GetOperationsHistoryByAccountId(int accountID);
        Task<int> GetSecondSideAccountID(int transactionID, int accountID);
    }
}
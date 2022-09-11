namespace CustomerAccount.Data.Interfaces
{
    public interface IOperationsHistoryData
    {
        Task<bool> AddOperation(OperationsHistory operation);
        Task<List<OperationsHistory>> GetOperationsHistories(int accountID);
        Task<int> GetOperationAccountID(int transactionID, int accountID);
    }
}
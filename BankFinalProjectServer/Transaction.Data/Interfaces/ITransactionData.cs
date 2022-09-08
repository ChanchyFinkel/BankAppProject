namespace Transaction.Data.Interfaces;
public interface ITransactionData
{
    Task<int> AddTransaction(Entities.Transaction transaction);
    Task<bool> UpdateTransactionStatus(int transactionID,Status transactionStatus,string? failureReason);
}
namespace Transaction.Data.Interfaces;
public interface ITransactionData
{
    Task AddTransaction(Entities.Transaction transaction);
    Task UpdateTransactionStatus(int transactionID,Status transactionStatus,string? failureReason);
}
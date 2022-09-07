
namespace Transaction.Service.Interfaces;
public interface ITransactionService
{
    Task AddTransaction(TransactionDTO transaction, IMessageSession messageSession, ClaimsPrincipal? User);
    Task UpdateTransactionStatus(int transactionID, bool success, string? failureReason);
}

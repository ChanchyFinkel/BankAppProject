namespace Transaction.Service.Interfaces;
public interface ITransactionService
{
    public Task AddTransaction(Data.Entities.Transaction transaction, IMessageSession messageSession);
}

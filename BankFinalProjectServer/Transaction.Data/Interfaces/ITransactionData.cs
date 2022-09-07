namespace Transaction.Data.Interfaces;
public interface ITransactionData
{
    Task<int> AddTransaction(Entities.Transaction transaction);
}
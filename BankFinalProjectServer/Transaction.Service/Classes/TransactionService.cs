namespace Transaction.Service.Classes;
public class TransactionService: ITransactionService
{
    private readonly ITransactionData _transactionData;
    static ILog log = LogManager.GetLogger<TransactionService>();
    public TransactionService(ITransactionData transactionData)
    {
        _transactionData = transactionData;
    }
    public async Task AddTransaction(Data.Entities.Transaction transaction,IMessageSession messageSession)
    {
        int transactionId = await _transactionData.AddTransaction(transaction);
        transaction.ID = transactionId; //to check if need it
        await PublishMessage(transaction, messageSession);
    }
    private Task PublishMessage(Data.Entities.Transaction transaction, IMessageSession context)
    {
        var _event = new TransactionStarted
        {
            TransactionID = transaction.ID,
            ToAccount = transaction.ToAccount,
            FromAccount = transaction.FromAccount,
            Ammount = transaction.Ammount
        };
        log.Info($"Sending TransactionStarted event, TransactionID = {_event.TransactionID}");
        return context.Publish(_event);
    }
}

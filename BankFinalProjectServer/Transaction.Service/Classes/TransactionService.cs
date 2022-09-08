namespace Transaction.Service.Classes;
public class TransactionService : ITransactionService
{
    private readonly ITransactionData _transactionData;
    private readonly IMapper _mapper;

    static ILog log = LogManager.GetLogger<TransactionService>();
    public TransactionService(ITransactionData transactionData, IMapper mapper)
    {
        _transactionData = transactionData;
        _mapper = mapper;
    }
    public async Task AddTransaction(TransactionDTO newTransaction, IMessageSession messageSession, ClaimsPrincipal? User)
    {
        Data.Entities.Transaction transaction = _mapper.Map<Data.Entities.Transaction>(newTransaction);
      //  int accountID = getAccountIDFromToken(User);
        //transaction.FromAccount=accountID;
        transaction.FromAccount = 100001;
        transaction.Date = DateTime.UtcNow;
        int transactionId = await _transactionData.AddTransaction(transaction);
        transaction.ID = transactionId; //to check if need it
        await PublishMessage(transaction, messageSession);
    }
    public int getAccountIDFromToken(ClaimsPrincipal User)
    {
        var accountID = User.Claims.First(x => x.Type.Equals("AccountID", StringComparison.InvariantCultureIgnoreCase)).Value;
        return int.Parse(accountID);
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

    public Task UpdateTransactionStatus(int transactionID, bool transactionStatus, string? failureReason)
    {
        Status status = transactionStatus? Status.SUCCESS:Status.FAIL;
        return _transactionData.UpdateTransactionStatus(transactionID, status,failureReason);
    }
}

namespace Transaction.WebApi.NSB;

public class TransactionPolicy : Saga<TransactionPolicyData>, IAmStartedByMessages<TransactionStarted>, IHandleMessages<TransfortDone>
{
    ITransactionService _transactionService;
    static ILog _log = LogManager.GetLogger<TransactionPolicy>();
    public TransactionPolicy(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public Task Handle(TransactionStarted message, IMessageHandlerContext context)
    {
        _log.Info($"TransactionStarted message received.");
        DoTransfort doTransfort = new DoTransfort()
        {
            TransactionID = message.TransactionID,
            FromAccount = message.FromAccount,
            ToAccount = message.ToAccount,
            Ammount = message.Ammount,
        };
        return context.Send(doTransfort);
    }

    public async Task Handle(TransfortDone message, IMessageHandlerContext context)
    {
        await _transactionService.UpdateTransactionStatus(message.TransactionID, message.Success,message.FailureReason);
        _log.Info("Saga Completed!");
        await Task.CompletedTask;
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.TransactionID)
                .ToMessage<TransactionStarted>(message => message.TransactionID)
                .ToMessage<TransfortDone>(message => message.TransactionID);
    }
}

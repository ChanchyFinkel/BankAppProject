namespace Account.WebApi.NSB;
public class DoTransfortHandler : IHandleMessages<DoTransfort>
{
    static ILog log = LogManager.GetLogger<DoTransfort>();
    private readonly IAccountService _accountService;
    public DoTransfortHandler(IAccountService AccountService)
    {
        _accountService = AccountService;
    }
    public async Task Handle(DoTransfort message, IMessageHandlerContext context)
    {
        log.Info("DoTransfort message received.");
        string failureReason=await _accountService.UpdateBalancesAndAddOperationsHistories(message.FromAccount, message.ToAccount, message.Ammount,message.TransactionID);
        TransfortDone transfortDone = new TransfortDone() { TransactionID = message.TransactionID };
        if (failureReason.Equals(""))
            transfortDone.Success = true;
        else
        {
            transfortDone.Success = false;
            transfortDone.FailureReason = failureReason;
        }
        await context.Publish(transfortDone);
    }
}
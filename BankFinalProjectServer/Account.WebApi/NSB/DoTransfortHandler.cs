namespace Account.NSB;
public class DoTransfortHandler : IHandleMessages<DoTransfort>
{
    static ILog log = LogManager.GetLogger<DoTransfort>();
    private readonly IAccountService _AccountService;
    private readonly IOperationsHistoryService _operationsHistoryService;
    private readonly IMapper _mapper;
    public DoTransfortHandler(IAccountService AccountService, IOperationsHistoryService operationsHistoryService, IMapper mapper)
    {
        _AccountService = AccountService;
        _operationsHistoryService = operationsHistoryService;
        _mapper = mapper;
    }
    public async Task Handle(DoTransfort message, IMessageHandlerContext context)
    {
        TransfortDone transfortDone = new TransfortDone() { TransactionID = message.TransactionID };
        log.Info("DoTransfort message received.");
        bool senderAccountIDIsValid = await _AccountService.ExistsAccountId(message.FromAccount);
        bool receiverAccountIDIsValid = await _AccountService.ExistsAccountId(message.ToAccount);
        bool senderBalanceIsEnough = await _AccountService.CheckSenderBalance(message.FromAccount, message.Ammount);
        if (senderAccountIDIsValid && receiverAccountIDIsValid && senderBalanceIsEnough)
        {
            bool updateBalanceSuccess = await _AccountService.UpdateReceiverAndSenderBalances(message.FromAccount, message.ToAccount, message.Ammount);
            if (updateBalanceSuccess)
            {
                OperationsHistory operationFromAccount = _mapper.Map<OperationsHistory>(message);
                OperationsHistory operationToAccount = _mapper.Map<OperationsHistory>(message);
                operationFromAccount.AccountID = message.FromAccount;
                operationFromAccount.Debit = true;
                operationFromAccount.OperationTime = DateTime.UtcNow;
                operationFromAccount.Balance = await _AccountService.GetAccountBalance(operationFromAccount.AccountID);
                bool operationFromAccountRes = await _operationsHistoryService.AddOperation(operationFromAccount);
                operationToAccount.AccountID = message.ToAccount;
                operationToAccount.Debit = false;
                operationToAccount.OperationTime = DateTime.UtcNow;
                operationToAccount.Balance = await _AccountService.GetAccountBalance(operationToAccount.AccountID);
                bool operationToAccountRes = await _operationsHistoryService.AddOperation(operationToAccount);
                if (operationToAccountRes && operationFromAccountRes)
                    transfortDone.Success = true;
                else
                {
                    transfortDone.Success = false;
                    transfortDone.FailureReason = !operationToAccountRes && !operationFromAccountRes ?
                        "Adding an entry to the operation history failed for the 2 accounts" : !operationToAccountRes ?
                        "Adding an entry to the operation history failed for reciever account" :
                        "Adding an entry to the operation history failed for sender account";
                }
            }
            else
            {
                transfortDone.Success = false;
                transfortDone.FailureReason = "update balance failed!";
            }
        }
        else
        {
            transfortDone.Success = false;
            transfortDone.FailureReason = !senderAccountIDIsValid ? "sender account ID is invalid!" :
            !receiverAccountIDIsValid ? "reciever account ID is invalid!" : "you don't have enough balance!";
        }
        await context.Publish(transfortDone);
    }
}
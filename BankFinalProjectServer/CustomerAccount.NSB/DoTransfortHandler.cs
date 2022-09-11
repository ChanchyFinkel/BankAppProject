namespace CustomerAccount.NSB;
public class DoTransfortHandler : IHandleMessages<DoTransfort>
{
    static ILog log = LogManager.GetLogger<DoTransfort>();
    private readonly ICustomerAccountService _customerAccountService;
    private readonly IOperationsHistoryService _operationsHistoryService;
    private readonly IMapper _mapper;
    public DoTransfortHandler(ICustomerAccountService customerAccountService, IOperationsHistoryService operationsHistoryService, IMapper mapper)
    {
        _customerAccountService = customerAccountService;
        _operationsHistoryService = operationsHistoryService;
        _mapper = mapper;
    }
    public async Task Handle(DoTransfort message, IMessageHandlerContext context)
    {
        TransfortDone transfortDone = new TransfortDone() { TransactionID = message.TransactionID };
        log.Info("DoTransfort message received.");
        bool senderAccountIDIsValid = await _customerAccountService.ExistsAccountId(message.FromAccount);
        bool receiverAccountIDIsValid = await _customerAccountService.ExistsAccountId(message.ToAccount);
        bool senderBalanceIsEnough = await _customerAccountService.CheckSenderBalance(message.FromAccount, message.Ammount);
        if (senderAccountIDIsValid && receiverAccountIDIsValid && senderBalanceIsEnough)
        {
            bool updateBalanceSuccess = await _customerAccountService.UpdateReceiverAndSenderBalances(message.FromAccount, message.ToAccount, message.Ammount);
            if (updateBalanceSuccess)
            {
                OperationsHistory operationFromAccount = _mapper.Map<OperationsHistory>(message);
                OperationsHistory operationToAccount = _mapper.Map<OperationsHistory>(message);
                operationFromAccount.AccountID = message.FromAccount;
                operationFromAccount.Debit = true;
                operationFromAccount.OperationTime = DateTime.UtcNow;
                operationFromAccount.Balance = await _customerAccountService.GetAccountBalance(operationFromAccount.AccountID);
                bool operationFromAccountRes= await _operationsHistoryService.AddOperation(operationFromAccount);
                operationToAccount.AccountID = message.ToAccount;
                operationToAccount.Debit = false;
                operationToAccount.OperationTime = DateTime.UtcNow;
                operationToAccount.Balance = await _customerAccountService.GetAccountBalance(operationToAccount.AccountID);
                bool operationToAccountRes=await _operationsHistoryService.AddOperation(operationToAccount);
                if(operationToAccountRes&&operationFromAccountRes)
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

namespace Account.Service.Classes;
public class AccountService : IAccountService
{
    private readonly IAccountData _AccountData;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public AccountService(IAccountData AccountData, IMapper mapper, IAuthService authService)
    {
        _AccountData = AccountData;
        _mapper = mapper;
        _authService = authService;
    }
    public Task<bool> IsExistAccountEmail(string email)
    {
        return _AccountData.ExistsAccountEmail(email);
    }
    public async Task<bool> CreateAccount(CustomerDTO customerDTO)
    {
        Customer newCustomer = _mapper.Map<CustomerDTO, Customer>(customerDTO);
        //await _AccountData.CreateCustomer(newCustomer);
        Data.Entities.Account newAccount = new Data.Entities.Account();
        newAccount.Customer = newCustomer;
        newAccount.OpenDate = DateTime.UtcNow;
        return await _AccountData.CreateAccount(newAccount,newCustomer,customerDTO.VerificationCode);
    }
    public async Task<AccountDTO> GetAccountInfo(ClaimsPrincipal user)
    {
        int accountID=_authService.getAccountIDFromToken(user);
        return _mapper.Map<AccountDTO>(await _AccountData.GetAccountInfo(accountID));
    }
    public Task<bool> ExistsAccountId(int accountID)
    {
        return _AccountData.ExistsAccountId(accountID);
    }
    public async Task<bool> CheckSenderBalance(int accountID,int amount)
    {
        int balance = await _AccountData.GetAccountBalance(accountID);
        return balance >= amount;
    }

    public Task<int> GetAccountBalance(int accountID)
    {
        return _AccountData.GetAccountBalance(accountID);
    }

    public Task<bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID, int ammount)
    {
        return _AccountData.UpdateReceiverAndSenderBalances(senderAccountID, recieverAccountID, ammount);
    }
    //public async Task<bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID, int ammount)
    //{
    //    bool senderAccountIDIsValid = await _AccountData.ExistsAccountId(senderAccountID);
    //    bool receiverAccountIDIsValid = await _AccountData.ExistsAccountId(recieverAccountID);
    //    bool senderBalanceIsEnough = await _AccountData.GetAccountBalance(senderAccountID) >= ammount;
    //    if (senderAccountIDIsValid && receiverAccountIDIsValid && senderBalanceIsEnough)
    //    {
    //        bool updateBalanceSuccess = await _AccountData.UpdateReceiverAndSenderBalances(senderAccountID, recieverAccountID, ammount);
    //        if (updateBalanceSuccess)
    //        {
    //            OperationsHistory operationFromAccount = _mapper.Map<OperationsHistory>(message);
    //            OperationsHistory operationToAccount = _mapper.Map<OperationsHistory>(message);
    //            operationFromAccount.AccountID = senderAccountID;
    //            operationFromAccount.Debit = true;
    //            operationFromAccount.OperationTime = DateTime.UtcNow;
    //            operationFromAccount.Balance = await _AccountData.GetAccountBalance(operationFromAccount.AccountID);
    //            bool operationFromAccountRes = await _operationsHistoryService.AddOperation(operationFromAccount);
    //            operationToAccount.AccountID = recieverAccountID;
    //            operationToAccount.Debit = false;
    //            operationToAccount.OperationTime = DateTime.UtcNow;
    //            operationToAccount.Balance = await _AccountData.GetAccountBalance(operationToAccount.AccountID);
    //            bool operationToAccountRes = await _operationsHistoryService.AddOperation(operationToAccount);
    //            if (operationToAccountRes && operationFromAccountRes)
    //                transfortDone.Success = true;
    //            else
    //            {
    //                transfortDone.Success = false;
    //                transfortDone.FailureReason = !operationToAccountRes && !operationFromAccountRes ?
    //                    "Adding an entry to the operation history failed for the 2 accounts" : !operationToAccountRes ?
    //                    "Adding an entry to the operation history failed for reciever account" :
    //                    "Adding an entry to the operation history failed for sender account";
    //            }
    //        }
    //        else
    //        {
    //            transfortDone.Success = false;
    //            transfortDone.FailureReason = "update balance failed!";
    //        }
    //    }
    //    else
    //    {
    //        transfortDone.Success = false;
    //        transfortDone.FailureReason = !senderAccountIDIsValid ? "sender account ID is invalid!" :
    //        !receiverAccountIDIsValid ? "reciever account ID is invalid!" : "you don't have enough balance!";
    //    }
    //    return _AccountData.UpdateReceiverAndSenderBalances(senderAccountID, recieverAccountID, ammount);
    //}

    public async Task<AccountHolderDTO> GetAccountHolderInfo(int accountNumber)
    {
        return _mapper.Map<AccountHolderDTO>(await _AccountData.GetAccountHolderInfo(accountNumber));
    }
}
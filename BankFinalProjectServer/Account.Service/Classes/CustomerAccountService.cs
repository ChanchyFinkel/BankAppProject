﻿namespace CustomerAccount.Service.Classes;
public class CustomerAccountService : ICustomerAccountService
{
    private readonly ICustomerAccountData _customerAccountData;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public CustomerAccountService(ICustomerAccountData customerAccountData, IMapper mapper, IAuthService authService)
    {
        _customerAccountData = customerAccountData;
        _mapper = mapper;
        _authService = authService;
    }
    public async Task<bool> CreateAccount(CustomerAccountDTO customerAccountDTO)
    {
        bool existAccount = await _customerAccountData.ExistsAccountEmail(customerAccountDTO.Email);
        if (existAccount)
            return false;
        Customer newCustomer = _mapper.Map<CustomerAccountDTO, Customer>(customerAccountDTO);
        await _customerAccountData.CreateCustomer(newCustomer);
        Account newAccount = new Account();
        newAccount.Customer = newCustomer;
        newAccount.OpenDate = DateTime.UtcNow;
        return await _customerAccountData.CreateAccount(newAccount);
    }
    public async Task<AccountDTO> GetAccountInfo(ClaimsPrincipal user)
    {
        int accountID=_authService.getAccountIDFromToken(user);
        return _mapper.Map<AccountDTO>(await _customerAccountData.GetAccountInfo(accountID));
    }
    public Task<bool> ExistsAccountId(int accountID)
    {
        return _customerAccountData.ExistsAccountId(accountID);
    }
    public async Task<bool> CheckSenderBalance(int accountID,int amount)
    {
        int balance = await _customerAccountData.GetAccountBalance(accountID);
        return balance >= amount;
    }

    public Task<int> GetAccountBalance(int accountID)
    {
        return _customerAccountData.GetAccountBalance(accountID);
    }

    public Task<bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID, int ammount)
    {
        return _customerAccountData.UpdateReceiverAndSenderBalances(senderAccountID, recieverAccountID, ammount);
    }
    //public async Task<bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID, int ammount)
    //{
    //    bool senderAccountIDIsValid = await _customerAccountData.ExistsAccountId(senderAccountID);
    //    bool receiverAccountIDIsValid = await _customerAccountData.ExistsAccountId(recieverAccountID);
    //    bool senderBalanceIsEnough = await _customerAccountData.GetAccountBalance(senderAccountID) >= ammount;
    //    if (senderAccountIDIsValid && receiverAccountIDIsValid && senderBalanceIsEnough)
    //    {
    //        bool updateBalanceSuccess = await _customerAccountData.UpdateReceiverAndSenderBalances(senderAccountID, recieverAccountID, ammount);
    //        if (updateBalanceSuccess)
    //        {
    //            OperationsHistory operationFromAccount = _mapper.Map<OperationsHistory>(message);
    //            OperationsHistory operationToAccount = _mapper.Map<OperationsHistory>(message);
    //            operationFromAccount.AccountID = senderAccountID;
    //            operationFromAccount.Debit = true;
    //            operationFromAccount.OperationTime = DateTime.UtcNow;
    //            operationFromAccount.Balance = await _customerAccountData.GetAccountBalance(operationFromAccount.AccountID);
    //            bool operationFromAccountRes = await _operationsHistoryService.AddOperation(operationFromAccount);
    //            operationToAccount.AccountID = recieverAccountID;
    //            operationToAccount.Debit = false;
    //            operationToAccount.OperationTime = DateTime.UtcNow;
    //            operationToAccount.Balance = await _customerAccountData.GetAccountBalance(operationToAccount.AccountID);
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
    //    return _customerAccountData.UpdateReceiverAndSenderBalances(senderAccountID, recieverAccountID, ammount);
    //}

    public async Task<AccountHolderDTO> GetAccountHolderInfo(int accountNumber)
    {
        return _mapper.Map<AccountHolderDTO>(await _customerAccountData.GetAccountHolderInfo(accountNumber));
    }
}
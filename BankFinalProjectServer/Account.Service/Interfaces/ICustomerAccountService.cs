namespace CustomerAccount.Service.Interfaces
{
    public interface ICustomerAccountService
    {
        Task<bool> CreateAccount(CustomerAccountDTO customerAccountDTO);
        Task<AccountDTO> GetAccountInfo(int accountID);
        Task<bool> ExistsAccountId(int accountID);
        Task<int> GetAccountBalance(int accountID);
        Task<bool> CheckSenderBalance(int accountID,int ammount);
        Task<bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID, int ammount);

    }
}
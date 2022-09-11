namespace CustomerAccount.Data.Interfaces
{
    public interface ICustomerAccountData
    {
        Task<bool> CreateAccount(Account account);
        Task CreateCustomer(Customer customer);
        Task<Account> GetAccountInfo(int accountID);
        Task<Account> GetAccountHolderInfo(int accountNumber);
        Task<bool> ExistsAccountEmail(string email);
        Task<bool> ExistsAccountId(int accountID);
        Task<int> GetAccountBalance(int accountID);
        Task <bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID, int ammount);
    }
}
namespace CustomerAccount.Data.Interfaces
{
    public interface ICustomerAccountData
    {
        Task<bool> CreateAccount(Account account);
        Task CreateCustomer(Customer customer);
        Task<Account> GetAccountInfo(int accountID);
        Task<bool> ExistsAccount(string email);
    }
}
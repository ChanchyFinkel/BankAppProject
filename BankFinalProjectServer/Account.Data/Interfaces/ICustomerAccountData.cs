namespace CustomerAccount.Data.Interfaces
{
    public interface ICustomerAccountData
    {
        Task<bool> CreateAccount(Account account);
        Task<bool> CreateCustomer(Customer customer);
        Task<Account> GetAccountInfo(string customerID);
        Task<bool> ExistsAccount(string email);
    }
}
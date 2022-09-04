namespace CustomerAccount.Service.Interfaces
{
    public interface ICustomerAccountService
    {
        Task<bool> CreateAccount(CustomerAccountDTO customerAccountDTO);
        Task<AccountDTO> GetAccountInfo(string customerID);
    }
}
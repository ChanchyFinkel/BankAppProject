namespace CustomerAccount.Service.Classes;
public class CustomerAccountService : ICustomerAccountService
{
    private readonly ICustomerAccountData _customerAccountData;
    public CustomerAccountService(ICustomerAccountData customerAccountData)
    {
        _customerAccountData = customerAccountData;
    }
    public async Task<bool> CreateAccount(CustomerAccountDTO customerAccountDTO)
    {

    }
    public async Task<AccountDTO> GetAccountInfo(string customerID)
    {

    }
}

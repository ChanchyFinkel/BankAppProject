namespace CustomerAccount.Service.Classes;
public class CustomerAccountService : ICustomerAccountService
{
    private readonly ICustomerAccountData _customerAccountData;
    private IMapper _mapper;

    public CustomerAccountService(ICustomerAccountData customerAccountData, IMapper mapper)
    {
        _customerAccountData = customerAccountData;
        _mapper = mapper;

    }
    public async Task<bool> CreateAccount(CustomerAccountDTO customerAccountDTO)
    {
        bool existAccount = await _customerAccountData.ExistsAccount(customerAccountDTO.Email);
        if (existAccount)
            return false;
        Customer newCustomer = _mapper.Map<CustomerAccountDTO,Customer>(customerAccountDTO);
        await _customerAccountData.CreateCustomer(newCustomer);
        Account newAccount = new Account();
        newAccount.Customer = newCustomer;
        newAccount.OpenDate = DateTime.UtcNow;
        return await _customerAccountData.CreateAccount(newAccount);
    }
    public async Task<AccountDTO> GetAccountInfo(int accountID)
    {
        return _mapper.Map<AccountDTO>(await _customerAccountData.GetAccountInfo(accountID));
    }
}
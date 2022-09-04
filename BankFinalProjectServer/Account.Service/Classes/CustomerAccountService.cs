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

    }
    public async Task<AccountDTO> GetAccountInfo(string customerID)
    {
        return _mapper.Map<AccountDTO>(await _customerAccountData.GetAccountInfo(customerID));
    }
}

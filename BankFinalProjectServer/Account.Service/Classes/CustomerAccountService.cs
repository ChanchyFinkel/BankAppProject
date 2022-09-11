namespace CustomerAccount.Service.Classes;
public class CustomerAccountService : ICustomerAccountService
{
    private readonly ICustomerAccountData _customerAccountData;
    private readonly IMapper _mapper;

    public CustomerAccountService(ICustomerAccountData customerAccountData, IMapper mapper)
    {
        _customerAccountData = customerAccountData;
        _mapper = mapper;

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
    public async Task<AccountDTO> GetAccountInfo(int accountID)
    {
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

    public Task<bool> UpdateReceiverAndSenderBalances(int senderAccountID, int recieverAccountID,  int ammount)
    {
        return _customerAccountData.UpdateReceiverAndSenderBalances(senderAccountID, recieverAccountID,  ammount);
    }
}
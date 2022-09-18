namespace Account.Service.Classes;
public class AccountService : IAccountService
{
    private readonly IAccountData _accountData;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IPasswordHashHelper _passwordHashHelper;
    private const int nIterations = 1000;
    private const int nHash = 8;

    public AccountService(IAccountData accountData, IMapper mapper, IAuthService authService, IPasswordHashHelper passwordHashHelper)
    {
        _accountData = accountData;
        _mapper = mapper;
        _authService = authService;
        _passwordHashHelper = passwordHashHelper;
    }
    public Task<bool> CreateAccount(CustomerDTO customerDTO)
    {
        Customer newCustomer = _mapper.Map<CustomerDTO, Customer>(customerDTO);
        newCustomer.Salt = _passwordHashHelper.GenerateSalt(8);
        newCustomer.Password = _passwordHashHelper.HashPassword(newCustomer.Password, newCustomer.Salt, nIterations, nHash);
        Data.Entities.Account newAccount = new Data.Entities.Account();
        newAccount.Customer = newCustomer;
        newAccount.OpenDate = DateTime.UtcNow;
        return _accountData.CreateAccount(newAccount, newCustomer, customerDTO.VerificationCode);
    }
    public async Task<AccountDTO> GetAccountInfo(ClaimsPrincipal user)
    {
        int accountID = _authService.getAccountIDFromToken(user);
        return _mapper.Map<Data.Entities.Account, AccountDTO>(await _accountData.GetAccountInfo(accountID));
    }
    public Task<int> GetAccountBalance(ClaimsPrincipal user)
    {
        int accountID = _authService.getAccountIDFromToken(user);
        return _accountData.GetAccountBalance(accountID);
    }
    public Task<string> UpdateBalancesAndAddOperationsHistory(int senderAccountID, int receiverAccountID, int ammount, int transactionID)
    {
        OperationsHistory senderOperation = new OperationsHistory() { TransactionID = transactionID, TransactionAmount = ammount, AccountID = senderAccountID, Debit = true, OperationTime = DateTime.UtcNow };
        OperationsHistory receiverOperation = new OperationsHistory() { TransactionID = transactionID, TransactionAmount = ammount, AccountID = receiverAccountID, Debit = false, OperationTime = DateTime.UtcNow };
        return _accountData.UpdateBalancesAndAddOperationsHistory(senderOperation, receiverOperation);
    }

    public async Task<SecondSideAccountDTO> GetSecondSideAccountInfo(int accountID)
    {
        return _mapper.Map<Data.Entities.Account,SecondSideAccountDTO>(await _accountData.GetAccountInfo(accountID));
    }
}
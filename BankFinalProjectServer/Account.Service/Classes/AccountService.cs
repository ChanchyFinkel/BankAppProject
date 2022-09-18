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
    public async Task<bool> CreateAccount(CustomerDTO customerDTO)
    {
        Customer newCustomer = _mapper.Map<CustomerDTO, Customer>(customerDTO);
        newCustomer.Salt = _passwordHashHelper.GenerateSalt(8);
        newCustomer.Password = _passwordHashHelper.HashPassword(newCustomer.Password, newCustomer.Salt, nIterations, nHash);
        Data.Entities.Account newAccount = new Data.Entities.Account();
        newAccount.Customer = newCustomer;
        newAccount.OpenDate = DateTime.UtcNow;
        return await _accountData.CreateAccount(newAccount, newCustomer, customerDTO.VerificationCode);
    }
    public async Task<AccountDTO> GetAccountInfo(ClaimsPrincipal user)
    {
        int accountID = _authService.getAccountIDFromToken(user);
        return _mapper.Map<AccountDTO>(await _accountData.GetAccountInfo(accountID));
    }
    public Task<int> GetAccountBalance(int accountID)
    {
        return _accountData.GetAccountBalance(accountID);
    }
    public Task<string> UpdateBalancesAndAddOperationsHistories(int senderAccountID, int receiverAccountID, int ammount, int transactionID)
    {
        OperationsHistory senderOperation = new OperationsHistory() { TransactionID = transactionID, TransactionAmount = ammount, AccountID = senderAccountID, Debit = true, OperationTime = DateTime.UtcNow };
        OperationsHistory receiverOperation = new OperationsHistory() { TransactionID = transactionID, TransactionAmount = ammount, AccountID = receiverAccountID, Debit = false, OperationTime = DateTime.UtcNow };
        return _accountData.UpdateBalancesAndAddOperationsHistories(senderOperation, receiverOperation);
    }

    public async Task<AccountHolderDTO> GetAccountHolderInfo(int accountNumber)
    {
        return _mapper.Map<AccountHolderDTO>(await _accountData.GetAccountHolderInfo(accountNumber));
    }
}
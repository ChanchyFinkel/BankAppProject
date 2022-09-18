namespace Account.Service.Interfaces;
public interface IAccountService
{
    Task<bool> CreateAccount(CustomerDTO AccountDTO);
    Task<AccountDTO> GetAccountInfo(ClaimsPrincipal user);
    Task<SecondSideAccountDTO> GetSecondSideAccountInfo(int accountNumber);
    Task<int> GetAccountBalance(ClaimsPrincipal user);
    Task<string> UpdateBalancesAndAddOperationsHistory(int senderAccountID, int receiverAccountID, int ammount, int transactionID);
}
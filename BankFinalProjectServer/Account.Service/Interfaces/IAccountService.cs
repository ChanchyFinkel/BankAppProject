namespace Account.Service.Interfaces;
public interface IAccountService
{
    Task<bool> CreateAccount(CustomerDTO AccountDTO);
    Task<AccountDTO> GetAccountInfo(ClaimsPrincipal user);
    Task<AccountHolderDTO> GetAccountHolderInfo(int accountNumber);
    Task<int> GetAccountBalance(int accountID);
    Task<string> UpdateBalancesAndAddOperationsHistories(int senderAccountID, int receiverAccountID, int ammount, int transactionID);
}
namespace Account.Service.Interfaces;
public interface IAccountService
{
    Task<bool> CreateAccount(CustomerDTO AccountDTO);
    Task<AccountDTO> GetAccountInfo(ClaimsPrincipal user);
    Task<AccountHolderDTO> GetAccountHolderInfo(int accountNumber);
    Task<bool> ExistsAccountId(int accountID);
    Task<int> GetAccountBalance(int accountID);
    Task<bool> CheckSenderBalance(int accountID, int ammount);
    Task<string> UpdateBalancesAndAddOperationsHistories(int senderAccountID, int receiverAccountID, int ammount, int transactionID);
    Task<bool> IsExistAccountEmail(string email);
}
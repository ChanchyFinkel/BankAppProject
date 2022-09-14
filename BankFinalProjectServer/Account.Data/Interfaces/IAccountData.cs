namespace Account.Data.Interfaces;

public interface IAccountData
{
    Task<bool> CreateAccount(Entities.Account account, Customer customer, int verificationCode);
    Task<Entities.Account> GetAccountInfo(int accountID);
    Task<Entities.Account> GetAccountHolderInfo(int accountNumber);
    Task<bool> ExistsAccountEmail(string email);
    Task<bool> ExistsAccountId(int accountID);
    Task<int> GetAccountBalance(int accountID);
    Task<string> UpdateBalancesAndAddOperationsHistories(OperationsHistory senderOperation, OperationsHistory receiverOperation);
}
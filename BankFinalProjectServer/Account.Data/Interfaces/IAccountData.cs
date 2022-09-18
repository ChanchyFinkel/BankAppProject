namespace Account.Data.Interfaces;

public interface IAccountData
{
    Task<bool> CreateAccount(Entities.Account account, Customer customer, int verificationCode);
    Task<Entities.Account> GetAccountInfo(int accountID);
    Task<int> GetAccountBalance(int accountID);
    Task<string> UpdateBalancesAndAddOperationsHistory(OperationsHistory senderOperation, OperationsHistory receiverOperation);
}
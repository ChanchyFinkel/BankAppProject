namespace CustomerAccount.Data.Interfaces;
public interface IAuthData
{
    Task<int> Login(string email, string password);
}
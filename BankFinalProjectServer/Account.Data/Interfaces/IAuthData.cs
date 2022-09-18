namespace Account.Data.Interfaces;
public interface IAuthData
{
    Task<Entities.Account> Login(string email);
}
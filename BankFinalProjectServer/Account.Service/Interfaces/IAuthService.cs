namespace CustomerAccount.Service.Interfaces
{
    public interface IAuthService
    {
        Task<int> Login(LoginDTO loginDTO);
    }
}
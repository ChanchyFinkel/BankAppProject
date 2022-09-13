namespace Account.Service.Interfaces;
public interface IAuthService
{
    Task<AuthDTO> Login(LoginDTO loginDTO);
    int getAccountIDFromToken(ClaimsPrincipal User);
}
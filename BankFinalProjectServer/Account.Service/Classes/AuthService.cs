namespace CustomerAccount.Service.Classes;

public class AuthService : IAuthService
{
    private readonly IAuthData _authData;
    public AuthService(IAuthData authData)
    {
        _authData = authData;
    }
    public async Task<int> Login(LoginDTO loginDTO)
    {

    }
}

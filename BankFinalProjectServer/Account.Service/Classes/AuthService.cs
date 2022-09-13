namespace Account.Service.Classes;
public class AuthService : IAuthService
{
    private readonly IAuthData _authData;
    private readonly IConfiguration _configuration;
    public AuthService(IAuthData authData, IConfiguration configuration)
    {
        _authData = authData;
        _configuration = configuration;
    }
    public async Task<AuthDTO> Login(LoginDTO loginDTO)
    {
        int accountID = await _authData.Login(loginDTO.Email, loginDTO.Password);
        if (accountID == 0)
            return null;
        string token = CreateToken(loginDTO.Email, accountID);
        AuthDTO authDTO = new AuthDTO() { AccountID = accountID, Token = token };
        return authDTO;
    }
    public int getAccountIDFromToken(ClaimsPrincipal User) {
        var accountID = User.Claims.First(x => x.Type.Equals("AccountID", StringComparison.InvariantCultureIgnoreCase)).Value;
        return int.Parse(accountID); 
    }

    private string CreateToken(string email, int accountID)
    {
        JwtSecurityToken token;
        var claims = new[] {
                          new Claim("UserEmail", email),
                          new Claim("AccountID", accountID.ToString()),
                          new Claim(ClaimTypes.Role, "user")
                    };

        var issuer = _configuration["JWT:Issuer"];
        var audience = _configuration["JWT:Audience"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signIn);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
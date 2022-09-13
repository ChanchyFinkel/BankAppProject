namespace Account.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<AuthDTO>> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            AuthDTO authDTO = await _authService.Login(loginDTO);
            return authDTO != null ? Ok(authDTO) : Unauthorized();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
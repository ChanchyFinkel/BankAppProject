namespace CustomerAccount.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult<int>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                int accountID = await _authService.Login(loginDTO);
                return accountID != 0 ? Ok(accountID):NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

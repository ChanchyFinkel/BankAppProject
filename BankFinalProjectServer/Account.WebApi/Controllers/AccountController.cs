namespace Account.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _AccountService;
    private readonly IAuthService _authService;
    public AccountController(IAccountService AccountService, IAuthService authService)
    {
        _AccountService = AccountService;
        _authService = authService;
    }

    [HttpPost]
    [Route("CreateAccount")]
    public async Task<ActionResult<bool>> CreateAccount([FromBody] CustomerDTO customerDTO)
    {
        try
        {
            bool succsess = await _AccountService.CreateAccount(customerDTO);
            return !succsess ? BadRequest(succsess) : Ok(succsess);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [Route("GetAccountInfo")]
    public async Task<ActionResult<AccountDTO>> GetAccountInfo()
    {
        try
        {
            AccountDTO accountDTO = await _AccountService.GetAccountInfo(User);
            return accountDTO != null ? Ok(accountDTO) : BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [Route("GetAccountHolderInfo/{accountNumber}")]
    public async Task<ActionResult<AccountHolderDTO>> GetAccountHolderInfo(int accountNumber)
    {
        try
        {
            AccountHolderDTO accountHolderDTO = await _AccountService.GetAccountHolderInfo(accountNumber);
            return accountHolderDTO != null ? Ok(accountHolderDTO) : BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [Route("GetAccountBalance")]
    public async Task<ActionResult<int>> GetAccountBalance()
    {
        try
        {
            int accountID = _authService.getAccountIDFromToken(User);
            int accountBalance = await _AccountService.GetAccountBalance(accountID);
            return accountBalance != 0 ? Ok(accountBalance) : BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

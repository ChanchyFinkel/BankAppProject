namespace Account.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("CreateAccount")]
    public async Task<ActionResult<bool>> CreateAccount([FromBody] CustomerDTO customerDTO)
    {
        try
        {
            bool succsess = await _accountService.CreateAccount(customerDTO);
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
            AccountDTO accountDTO = await _accountService.GetAccountInfo(User);
            return accountDTO != null ? Ok(accountDTO) : BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [Route("GetSecondSideAccountInfo/{accountID}")]
    public async Task<ActionResult<SecondSideAccountDTO>> GetSecondSideAccountInfo(int accountID)
    {
        try
        {
            SecondSideAccountDTO secondSideAccountDTO = await _accountService.GetSecondSideAccountInfo(accountID);
            return secondSideAccountDTO != null ? Ok(secondSideAccountDTO) : BadRequest();
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
            int accountBalance = await _accountService.GetAccountBalance(User);
            return accountBalance != 0 ? Ok(accountBalance) : BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

namespace CustomerAccount.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IAuthService _authService;
        public CustomerAccountController(ICustomerAccountService customerAccountService, IAuthService authService)
        {
            _customerAccountService = customerAccountService;
            _authService = authService;
        }
        [HttpPost]
        [Route("CreateAccount")]
        public async Task<ActionResult<bool>> CreateAccount([FromBody] CustomerAccountDTO customerAccountDTO)
        {
            try
            {
                bool succsess = await _customerAccountService.CreateAccount(customerAccountDTO);
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
                AccountDTO accountDTO = await _customerAccountService.GetAccountInfo(User);
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
                AccountHolderDTO accountHolderDTO = await _customerAccountService.GetAccountHolderInfo(accountNumber);
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
                int accountBalance = await _customerAccountService.GetAccountBalance(accountID);
                return accountBalance != 0 ? Ok(accountBalance) : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

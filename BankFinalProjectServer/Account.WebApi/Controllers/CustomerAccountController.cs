namespace CustomerAccount.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IAuthService _authService;
        public CustomerAccountController(ICustomerAccountService customerAccountService,IAuthService authService)
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
            catch(Exception ex)
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
                int accountID=_authService.getAccountIDFromToken(User);
                AccountDTO accountDTO = await _customerAccountService.GetAccountInfo(accountID);
                return accountDTO != null ? Ok(accountDTO) : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

namespace CustomerAccount.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;
        public CustomerAccountController(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }
        [HttpPost]
        [Route("CreateAccount")]
        public async Task<bool> CreateAccount([FromBody] CustomerAccountDTO customerAccountDTO)
        {

        }

        [HttpGet]
        [Route("GetAccountInfo/{accountID}")]
        public async Task<ActionResult<AccountDTO>> GetAccountInfo(string accountID)
        {
            try
            {
                AccountDTO accountDTO = await _customerAccountService.GetAccountInfo(accountID);
                return accountDTO == null ? Ok(accountDTO):BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

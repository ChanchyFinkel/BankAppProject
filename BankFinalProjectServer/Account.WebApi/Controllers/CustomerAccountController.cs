

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
        [HttpGet]
        [Route("GetAccountInfo/{accountID}")]
        public async Task<ActionResult<AccountDTO>> GetAccountInfo(int accountID)
        {
            try
            {
                AccountDTO accountDTO = await _customerAccountService.GetAccountInfo(accountID);
                return accountDTO == null ? Ok(accountDTO) : BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<AccountDTO> GetAccountInfo(string accountID)
        {

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Transaction.Service.Interfaces;

namespace Transaction.WebApi.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMessageSession _messageSession;
        public TransactionController(ITransactionService transactionService, IMessageSession messageSession)
        {
            _transactionService = transactionService;
            _messageSession = messageSession;
        }
        [HttpPost]
        [Route("AddTransaction")]
        public async Task<ActionResult> AddTransaction([FromBody] Data.Entities.Transaction transaction)
        {
            try
            {
                await _transactionService.AddTransaction(transaction, _messageSession);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
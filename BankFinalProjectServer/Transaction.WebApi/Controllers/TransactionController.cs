namespace Transaction.WebApi.Controllers;

[Authorize]
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
    public async Task<ActionResult> AddTransaction([FromBody] TransactionDTO transaction)
    {
        try
        {
            await _transactionService.AddTransaction(transaction, _messageSession ,null);
            return Accepted();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
namespace Account.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OperationsHistoryController : ControllerBase
{
    private readonly IOperationsHistoryService _operationsHistoryService;
    private readonly IConverter _converter;
    public OperationsHistoryController(IOperationsHistoryService operationsHistoryService, IConverter converter)
    {
        _operationsHistoryService = operationsHistoryService;
        _converter = converter;
    }

    [HttpGet]
    [Route("GetOperationsHistories/{pageSize}/{page}")]
    public async Task<ActionResult<OperationDataListDTO>> GetOperationsHistories( int pageSize, int page)
    {
        try
        {
            return Ok(await _operationsHistoryService.GetOperationsHistories(User, pageSize, page));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetOperationsHistoriesAsPDF/{month}/{year}")]
    public async Task<IActionResult> CreateOperationsHistoriesPDF(int month,int year)
    {
        try
        {
            byte[] file = await _operationsHistoryService.CreateOperationsHistoriesPDF(month, year, _converter, User);
            var res = File(file, "application/pdf", "OperationsHistories.pdf");
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
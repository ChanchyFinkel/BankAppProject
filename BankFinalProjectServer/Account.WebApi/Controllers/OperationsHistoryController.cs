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
    [Route("GetOperationsHistory/{pageSize}/{page}")]
    public async Task<ActionResult<OperationDataListDTO>> GetOperationsHistory(int pageSize, int page)
    {
        try
        {
            OperationDataListDTO operationDataListDTO = await _operationsHistoryService.GetOperationsHistory(User, pageSize, page);
            return operationDataListDTO.TotalRows == 0 ? NoContent() : Ok(operationDataListDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetOperationsHistoryAsPDF/{month}/{year}")]
    public async Task<IActionResult> GetOperationsHistoryAsPDF(int month, int year)
    {
        try
        {
            byte[] file = await _operationsHistoryService.CreateOperationsHistoryPDF(month, year, _converter, User);
            //var res = File(file, "application/pdf", "OperationsHistories.pdf");
            return Ok(file);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
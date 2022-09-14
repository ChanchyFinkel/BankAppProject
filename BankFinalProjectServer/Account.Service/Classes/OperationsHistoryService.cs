namespace Account.Service.Classes;
public class OperationsHistoryService : IOperationsHistoryService
{
    private readonly IOperationsHistoryData _operationsHistoryData;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public OperationsHistoryService(IOperationsHistoryData operationsHistoryData, IMapper mapper, IAuthService authService)
    {
        _operationsHistoryData = operationsHistoryData;
        _mapper = mapper;
        _authService = authService;
    }
    public async Task<OperationDataListDTO> GetOperationsHistories(ClaimsPrincipal User, int pageSize, int page)
    {
        int accountID = _authService.getAccountIDFromToken(User);
        List<OperationsHistory> operationsHistories = await _operationsHistoryData.GetOperationsHistories(accountID);
        OperationDataListDTO operationsDataListDTO = new OperationDataListDTO();
        operationsDataListDTO.TotalRows = operationsHistories.Count;
        operationsHistories = operationsHistories.Skip(pageSize * page).Take(pageSize).ToList();
        List<OperationsHistoryDTO> operationsHistoriesDTO = new List<OperationsHistoryDTO>();
        foreach (var operation in operationsHistories)
        {
            OperationsHistoryDTO operationDTO = _mapper.Map<OperationsHistoryDTO>(operation);
            operationDTO.AccountNumber = await _operationsHistoryData.GetOperationAccountID(operation.TransactionID, operation.AccountID);
            operationsHistoriesDTO.Add(operationDTO);
        }
        operationsDataListDTO.Operations = operationsHistoriesDTO;
        return operationsDataListDTO;
    }
}

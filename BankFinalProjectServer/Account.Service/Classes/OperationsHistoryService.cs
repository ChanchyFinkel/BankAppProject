namespace CustomerAccount.Service.Classes;
public class OperationsHistoryService : IOperationsHistoryService
{
    private readonly IOperationsHistoryData _operationsHistoryData;
    private readonly IMapper _mapper;
    public OperationsHistoryService(IOperationsHistoryData operationsHistoryData, IMapper mapper)
    {
        _operationsHistoryData = operationsHistoryData;
        _mapper = mapper;
    }
    public async Task<OperationDataListDTO> GetOperationsHistories(int accountID, int pageSize, int page)
    {
        List<OperationsHistory> operationsHistories = await _operationsHistoryData.GetOperationsHistories(accountID);
        OperationDataListDTO operationsDataListDTO = new OperationDataListDTO();
        operationsDataListDTO.TotalRows = operationsHistories.Count;
        operationsHistories = operationsHistories.Skip(pageSize * (page-1)).Take(pageSize).ToList();
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
    public Task<bool> AddOperation(OperationsHistory operation)
    {
        return _operationsHistoryData.AddOperation(operation);
    }
}

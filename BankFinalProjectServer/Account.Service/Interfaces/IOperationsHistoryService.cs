namespace Account.Service.Interfaces;
public interface IOperationsHistoryService
{
    Task<bool> AddOperation(OperationsHistory operation);
    Task<OperationDataListDTO> GetOperationsHistories(ClaimsPrincipal User,int pageSize, int page);
}
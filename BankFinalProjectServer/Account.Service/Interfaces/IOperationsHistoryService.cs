namespace Account.Service.Interfaces;
public interface IOperationsHistoryService
{
    Task<OperationDataListDTO> GetOperationsHistories(ClaimsPrincipal User,int pageSize, int page);
}
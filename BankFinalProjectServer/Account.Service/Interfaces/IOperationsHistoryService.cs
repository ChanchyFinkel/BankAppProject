namespace Account.Service.Interfaces;
public interface IOperationsHistoryService
{
    Task<OperationDataListDTO> GetOperationsHistories(ClaimsPrincipal User,int pageSize, int page);
    //Task<byte[]> CreateOperationsHistoriesPDF(int month, int year, IConverter _converter, ClaimsPrincipal User);
    Task<byte[]> CreateOperationsHistoriesPDF(int month, int year, IConverter _converter, ClaimsPrincipal User);
}
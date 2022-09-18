namespace Account.Service.Interfaces;
public interface IOperationsHistoryService
{
    Task<OperationDataListDTO> GetOperationsHistory(ClaimsPrincipal User,int pageSize, int page);
    Task<byte[]> CreateOperationsHistoryPDF(int month, int year, IConverter _converter, ClaimsPrincipal User);
}
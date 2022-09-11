namespace CustomerAccount.Service.Interfaces
{
    public interface IOperationsHistoryService
    {
        Task<bool> AddOperation(OperationsHistory operation);
        Task<OperationDataListDTO> GetOperationsHistories(int accountID, int pageSize, int page);
    }
}
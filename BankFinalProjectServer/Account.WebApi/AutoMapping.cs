namespace CustomerAccount.WebApi;
public class AutoMapping:Profile
{
    public AutoMapping()
    {
        CreateMap<CustomerAccountDTO, Customer>();
        CreateMap<Account,AccountDTO>().ForMember(dest =>
            dest.AccountID,
            opt => opt.MapFrom(src => src.ID))
            .ForMember(dest =>
            dest.FirstName,
            opt => opt.MapFrom(src => src.Customer.FirstName))
        .ForMember(dest =>
            dest.LastName,
            opt => opt.MapFrom(src => src.Customer.LastName));
        CreateMap<OperationsHistory, OperationsHistoryDTO>().ForMember(dest =>
            dest.Amount,
            opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest =>
            dest.Date,
            opt => opt.MapFrom(src => src.OperationTime)).ReverseMap();
    }
}

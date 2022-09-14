namespace Account.WebApi;
public class AutoMapping:Profile
{
    public AutoMapping()
    {
        CreateMap<CustomerDTO, Customer>();
        CreateMap<Data.Entities.Account,AccountDTO>().ForMember(dest =>
            dest.AccountID,
            opt => opt.MapFrom(src => src.ID))
            .ForMember(dest =>
            dest.FirstName,
            opt => opt.MapFrom(src => src.Customer.FirstName))
        .ForMember(dest =>
            dest.LastName,
            opt => opt.MapFrom(src => src.Customer.LastName));
        CreateMap<Data.Entities.Account, AccountHolderDTO>()
           .ForMember(dest =>
           dest.FirstName,
           opt => opt.MapFrom(src => src.Customer.FirstName))
       .ForMember(dest =>
           dest.LastName,
           opt => opt.MapFrom(src => src.Customer.LastName))
       .ForMember(dest =>
           dest.Email,
           opt => opt.MapFrom(src => src.Customer.Email));
        CreateMap<OperationsHistory, OperationsHistoryDTO>().ForMember(dest =>
            dest.Amount,
            opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest =>
            dest.Date,
            opt => opt.MapFrom(src => src.OperationTime)).ReverseMap();
    }
}

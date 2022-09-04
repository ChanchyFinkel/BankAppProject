

namespace CustomerAccount.Service;
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
    }

}

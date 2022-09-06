using AutoMapper;
using Transection.DTO;

namespace CustomerAccount.Service;
public class AutoMapping:Profile
{
    public AutoMapping()
    {
        CreateMap<TransactionDTO, Transaction.Data.Entities.Transaction>();
    }

}

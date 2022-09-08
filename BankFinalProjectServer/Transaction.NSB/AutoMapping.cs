namespace Transaction.NSB;
public class AutoMapping:Profile
{
    public AutoMapping()
    {
        CreateMap<TransactionDTO, Data.Entities.Transaction>();
    }
}

namespace Account.DTO;
public class OperationsHistoryDTO
{
    public int SecondSideAccountID { get; set; }
    public bool Debit { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
    public DateTime Date { get; set; }
}

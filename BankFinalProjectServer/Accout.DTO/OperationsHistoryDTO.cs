namespace CustomerAccount.DTO;
public class OperationsHistoryDTO
{
    public int AccountNumber { get; set; }
    [Required]
    public bool Debit { get; set; }
    [Required]
    [Range(1, 1000000)]
    public int Amount { get; set; }
    [Required]
    public int Balance { get; set; }
    [Required]
    public DateTime Date { get; set; }
}

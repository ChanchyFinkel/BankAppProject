namespace Transection.DTO;

public class TransactionDTO
{
    [Required]
    public int ToAccount { get; set; }
    [Required]
    [Range(1, 1000000)]
    public int Ammount { get; set; }
}

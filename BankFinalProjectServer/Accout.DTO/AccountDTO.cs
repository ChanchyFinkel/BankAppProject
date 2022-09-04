namespace CustomerAccount.DTO;
public class AccountDTO
{
    [Required]
    public int AccountID { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public decimal Balance { get; set; } = 1000;
}

namespace Account.DTO;
public class AccountDTO
{
    public int AccountID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Balance { get; set; } = 1000;
}

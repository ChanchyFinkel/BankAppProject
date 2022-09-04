namespace CustomerAccount.DTO;
public class CustomerAccountDTO
{
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(40)]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(25)]
    public string Password { get; set; }
}

namespace Account.DTO;
public class LoginDTO
{
    [Required]
    [EmailAddress]
    [MinLength(5)]
    [MaxLength(40)]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(25)]
    public string Password { get; set; }
}
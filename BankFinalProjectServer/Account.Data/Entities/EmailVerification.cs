namespace Account.Data.Entities;
public class EmailVerification
{
    [Key]
    [MaxLength(40)]
    public string Email { get; set; }
    [Required]
    [Range(1000,9999)]
    public int VerificationCode { get; set; }
    [Required]
    public DateTime ExpirationTime  { get; set; }
}

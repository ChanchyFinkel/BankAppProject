namespace Account.Data.Entities;

[Index("Email", IsUnique = true)]
public class Customer
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(40)]
    public string Email { get; set; }
    [Required]
    [MaxLength(25)]
    public string Password { get; set; }
}

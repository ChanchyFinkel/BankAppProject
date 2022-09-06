namespace CustomerAccount.Data.Entities;
public class Account
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [ForeignKey("Customer")]
    [Required]
    public Guid CustomerID { get; set; }
    public virtual Customer Customer { get; set; }
    [Required]
    public DateTime OpenDate { get; set; }
    [Required]
    public decimal Balance { get; set; } = 1000;
}

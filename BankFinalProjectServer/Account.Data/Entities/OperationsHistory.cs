namespace Account.Data.Entities;
public class OperationsHistory
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [ForeignKey("Account")]
    [Required]
    public int AccountID { get; set; }
    public virtual Account Account { get; set; }
    [Required]
    public int TransactionID { get; set; }
    [Required]
    public bool Debit { get; set; }
    [Required]
    [Range(1,1000000)]
    public int TransactionAmount { get; set; }
    [Required]
    public int Balance { get; set; }
    [Required]
    public DateTime OperationTime { get; set; }
}

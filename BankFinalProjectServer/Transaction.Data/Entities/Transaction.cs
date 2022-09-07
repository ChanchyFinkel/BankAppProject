using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transaction.Data.Entities;
public enum Status
{
    PROCESSING,SUCCESS,FAIL
}
public class Transaction
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Required]
    public int FromAccount { get; set; }
    [Required]
    public int ToAccount { get; set; }
    [Required]
    [Range(1,1000000)]
    public int Ammount { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public Status Status { get; set; }=Status.PROCESSING;
    public string? FailureReason { get; set; }
}
﻿using System.ComponentModel.DataAnnotations;
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
    [Range(1,1000000)]
    public int Ammount { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; }
    public string FailureReason { get; set; }
}
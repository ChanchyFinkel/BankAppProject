namespace Messages.NSB.Events;

public class TransfortDone
{
    public int TransactionID { get; set; }
    public bool Success { get; set; }
    public string? FailureReason { get; set; }
}


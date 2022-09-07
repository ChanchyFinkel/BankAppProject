namespace Transaction.Data.Classes;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }
    public virtual DbSet<Entities.Transaction> Transaction { get; set; }
}



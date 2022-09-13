namespace Account.Data.Classes;
public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }
    public virtual DbSet<Entities.Account> Account { get; set; }
    public virtual DbSet<Customer> Customer { get; set; }
    public virtual DbSet<OperationsHistory> OperationsHistory { get; set; }
    public virtual DbSet<EmailVerification> EmailVerification { get; set; }
}



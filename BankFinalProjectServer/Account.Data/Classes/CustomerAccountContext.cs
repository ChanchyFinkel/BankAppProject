namespace CustomerAccount.Data.Classes;
public class CustomerAccountContext : DbContext
{
    public CustomerAccountContext(DbContextOptions<CustomerAccountContext> options) : base(options) { }
    public virtual DbSet<Account> Account { get; set; }
    public virtual DbSet<Customer> Customer { get; set; }
}



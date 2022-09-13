namespace Account.Data.Classes;
public class AuthData : IAuthData
{
    private readonly IDbContextFactory<AccountContext> _factory;
    public AuthData(IDbContextFactory<AccountContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }
    public async Task<int> Login(string email, string password)
    {
        using var context = _factory.CreateDbContext();
        var account = await context.Account.Include(account => account.Customer).FirstOrDefaultAsync(account => account.Customer.Email.Equals(email) && account.Customer.Password.Equals(password));
        return account == null ? 0 : account.ID;
    }
}

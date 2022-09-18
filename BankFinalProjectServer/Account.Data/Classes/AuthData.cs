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
    public async Task<Entities.Account> Login(string email)
    {
        using var context = _factory.CreateDbContext();
        return await context.Account.Include(account => account.Customer).FirstAsync(account => account.Customer.Email.Equals(email));
    }
}
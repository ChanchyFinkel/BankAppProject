namespace Account.Data.Classes;

public class EmailVerificationData : IEmailVerificationData
{
    private readonly IDbContextFactory<AccountContext> _factory;
    public EmailVerificationData(IDbContextFactory<AccountContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }
    public async Task<bool> AddEmailVerification(EmailVerification emailVerification)
    {
        try
        {
            var context = _factory.CreateDbContext();
            bool existAccount = await context.Customer.AnyAsync(c => c.Email.Equals(emailVerification.Email));
            if (!existAccount)
                return false;
            await context.AddAsync(emailVerification);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<EmailVerification> GetEmailVerification(string email)
    {
        var context = _factory.CreateDbContext();
        EmailVerification emailVerification = await context.EmailVerification.Where(e => e.Email.Equals(email)).OrderByDescending(e => e.ExpirationTime).FirstAsync();
        return emailVerification;
    }
}

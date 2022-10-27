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
        var context = _factory.CreateDbContext();
        bool existsAccount = await context.Customer.AnyAsync(c => c.Email.Equals(emailVerification.Email));
        if (existsAccount)
            return false;
        var existsEmailVerification = await context.EmailVerification.FirstOrDefaultAsync(c => c.Email.Equals(emailVerification.Email));
        if (existsEmailVerification != null)
        {
            existsEmailVerification.ExpirationTime = emailVerification.ExpirationTime;
            existsEmailVerification.VerificationCode = emailVerification.VerificationCode;
        }
        else
        {
            await context.AddAsync(emailVerification);
        }
        await context.SaveChangesAsync();
        return true;
    }
}
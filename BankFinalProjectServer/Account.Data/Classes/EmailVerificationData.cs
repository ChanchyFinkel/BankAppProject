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
            if (existAccount)
                return false;
            EmailVerification e= await context.EmailVerification.FirstOrDefaultAsync(c => c.Email.Equals(emailVerification.Email));
            if (e != null)
            {
                e.ExpirationTime = emailVerification.ExpirationTime;
                e.VerificationCode = emailVerification.VerificationCode;
            }
            else
            {
                await context.AddAsync(emailVerification);
            }
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
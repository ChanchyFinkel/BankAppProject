namespace Account.Service.Interfaces;

public interface IEmailVerificationService
{
    Task AddEmailVerification(string email);
    Task<EmailVerification> GetEmailVerification(string email);
}
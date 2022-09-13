namespace Account.Service.Interfaces;

public interface IEmailVerificationService
{
    Task SendEmailVerification(string email);
}
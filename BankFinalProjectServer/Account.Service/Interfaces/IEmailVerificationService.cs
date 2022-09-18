namespace Account.Service.Interfaces;

public interface IEmailVerificationService
{
    Task CreateEmailVerification(string email);
}
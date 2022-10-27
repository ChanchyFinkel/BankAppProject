namespace Account.Data.Interfaces;

public interface IEmailVerificationData
{
    Task<bool> AddEmailVerification(EmailVerification emailVerification);
}
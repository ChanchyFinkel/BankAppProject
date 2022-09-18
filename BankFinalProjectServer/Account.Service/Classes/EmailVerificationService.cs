namespace Account.Service.Classes;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IEmailVerificationData _emailVerificationData;
    private const int experationTime = 5;
    private int verificationCode;
    public EmailVerificationService(IEmailVerificationData emailVerificationData)
    {
        _emailVerificationData = emailVerificationData;
    }
    public async Task CreateEmailVerification(string email)
    {
        Random random = new Random();
        verificationCode = random.Next(1000, 9999);
        EmailVerification emailVerification = new EmailVerification() { Email = email, VerificationCode = verificationCode, ExpirationTime = DateTime.UtcNow.AddMinutes(experationTime) };
        bool success = await _emailVerificationData.AddEmailVerification(emailVerification);
        if (success)
        {
            SendEmailVerfication(email);
        }
    }
    private void SendEmailVerfication(string email)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress("mytravelProject22@gmail.com");
        message.To.Add(new MailAddress(email));
        message.Subject = "Verfication code send from BankApp";
        message.Body = $" Your verfication code is {verificationCode} The code is valid for {experationTime} minutes ";
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        System.Net.NetworkCredential basicCredential1 = new
        System.Net.NetworkCredential("mytravelProject22@gmail.com", "kxbfxdziccmrcweo");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicCredential1;
        try
        {
            client.Send(message);
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

}

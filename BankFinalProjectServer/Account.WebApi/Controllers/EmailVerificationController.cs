namespace Account.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailVerificationController : ControllerBase
{
    private readonly IEmailVerificationService _emailVerificationService;
    public EmailVerificationController(IEmailVerificationService emailVerificationService)
    {
        _emailVerificationService = emailVerificationService;
    }

    [HttpPost]
    [Route("SendEmailVerification")]
    public ActionResult SendEmailVerification([FromBody]string email)
    {
        try
        {
            _emailVerificationService.SendEmailVerification(email);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    } 
}

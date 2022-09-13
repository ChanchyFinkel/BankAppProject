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
    public ActionResult AddEmailVerification([FromBody] string email)
    {
        try
        {
            _emailVerificationService.AddEmailVerification(email);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    } 
}

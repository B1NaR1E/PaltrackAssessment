namespace Assessment.Api.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController(IIdentityManager manager) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> UserLoginAsync(LoginRequest request)
    {
        try
        {
            var response = await manager
                .AuthUserByCredentialsAsync(request);

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequestActionResult(e.Message);
        }
    }
}
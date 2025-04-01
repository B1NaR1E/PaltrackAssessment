using Microsoft.Extensions.Options;

namespace Assessment.Api.Services;

public class IdentityManager(ITokenRequester tokenRequester,
    UserManager<ApplicationUser> userManager,
    IOptions<TokenIssuerConfig> issuerSettings,
    RoleManager<IdentityRole> roleManager) : IIdentityManager
{
    public async Task<LoginResponse> AuthUserByCredentialsAsync(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Username) ??
                   throw new Exception("Invalid username or password.");

        if (user.IsDeleted)
            throw new Exception("Invalid username or password.");
        
        var response = await tokenRequester.GetUserTokenAsync(
            issuerSettings.Value,
            request.Username,
            request.Password);

        if(response.HttpStatusCode == HttpStatusCode.BadRequest)
            throw new ApplicationException("Invalid username or password.");

        return new LoginResponse(user.Id, user.UserName, response.AccessToken, response.RefreshToken);
    }
}

public record LoginResponse(string UserId, string? Username, string? AccessToken, string? RefreshToken);
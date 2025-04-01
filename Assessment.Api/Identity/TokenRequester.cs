namespace Assessment.Api.Identity;

public class TokenRequester(IHttpClientFactory factory) : ITokenRequester
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    public async Task<TokenResponse> GetUserTokenAsync(TokenIssuerConfig settings, string userName, string password)
    {
        var identityServerAddress = $"{settings.Authority}/connect/token";
        var response = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = identityServerAddress,
            ClientId = settings.ClientId,
            ClientSecret = settings.ClientSecret,
            Scope = settings.Scope,
            GrantType = "password",
            UserName = userName,
            Password = password
        });

        return response;
    }
}
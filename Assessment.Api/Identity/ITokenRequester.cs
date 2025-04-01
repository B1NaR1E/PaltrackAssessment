namespace Assessment.Api.Identity;

public interface ITokenRequester
{
    Task<TokenResponse> GetUserTokenAsync(TokenIssuerConfig settings, string userName, string password);
}
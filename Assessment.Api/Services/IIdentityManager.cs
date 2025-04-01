namespace Assessment.Api.Services;

public interface IIdentityManager
{
    Task<LoginResponse> AuthUserByCredentialsAsync(LoginRequest request);
}
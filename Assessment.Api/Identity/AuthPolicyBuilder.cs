namespace Assessment.Api.Identity;

public static class AuthPolicyBuilder
{
    public static AuthorizationPolicy CanRead =>
        new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("scope", "read")
            .Build();
}
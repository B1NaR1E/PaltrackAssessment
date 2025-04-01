namespace Assessment.Api;

public class ClaimsRequirementHandler : AuthorizationHandler<ClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
        var claim = context.User.Claims
            .FirstOrDefault(c => c.Type == requirement.ClaimName);

        if (claim is not null && claim.Value.Contains(requirement.ClaimValue))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
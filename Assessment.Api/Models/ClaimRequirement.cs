namespace Assessment.Api.Models;

public class ClaimRequirement(string claimName, string claimValue) 
    : IAuthorizationRequirement
{
    public string ClaimName { get; set; } = claimName;
    public string ClaimValue { get; set; } = claimValue;
}
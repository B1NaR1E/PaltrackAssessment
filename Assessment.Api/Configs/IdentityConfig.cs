using Duende.IdentityServer;

namespace Assessment.Api.Configs;

public static class IdentityConfig
{
    private const string ApiScope = "assessment-api.scope";
    private const string ReadScope = "read";
    private const string WriteScope = "write";
    private const string DeleteScope = "delete";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new("assessment-api")
            {
                Scopes = new List<string>
                {
                    ApiScope,
                    ReadScope,
                    WriteScope,
                    DeleteScope
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new(ApiScope, "assessment-api"),
            new(name: ReadScope, displayName: "Read your data."),
            new(name: WriteScope, displayName: "Write your data."),
            new(name: DeleteScope, displayName: "Delete your data."),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // User's client
            new()
            {
                ClientId = "assessment_api.user_client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = true,
                ClientSecrets = new List<Secret>
                {
                    new("secret234554^&%&^%&^f2%%%".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    ReadScope,
                    WriteScope,
                    DeleteScope
                },
                AccessTokenLifetime = 86400
            }
        };

    public static IEnumerable<IdentityRole> DefaultRoles =>
    [
        new(PoliciesAndRoles.Roles.User) { NormalizedName = PoliciesAndRoles.Roles.User.ToUpper() }
    ];
}
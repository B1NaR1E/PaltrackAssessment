using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;

namespace Assessment.Api.Database;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<IdentityDbContext>().Database.Migrate();
        scope.ServiceProvider
            .GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        using var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        context.Database.Migrate();

        if (!context.Clients.Any())
        {
            foreach (var client in IdentityConfig.Clients)
                context.Clients.Add(client.ToEntity());

            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in IdentityConfig.IdentityResources)
                context.IdentityResources.Add(resource.ToEntity());

            context.SaveChanges();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var resource in IdentityConfig.ApiResources)
                context.ApiResources.Add(resource.ToEntity());

            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var apiScope in IdentityConfig.ApiScopes)
                context.ApiScopes.Add(apiScope.ToEntity());

            context.SaveChanges();
        }
        
        using var identityContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

        if (!identityContext.Roles.Any())
        {
            foreach (var role in IdentityConfig.DefaultRoles)
                identityContext.Roles.Add(role);

            identityContext.SaveChanges();
        }

        return host;
    }

    public static IHost CreateAdminUserIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var adminUser = userManager.FindByNameAsync("admin@admin.com").Result;
        
        if(adminUser is not null)
            return host;
        
        var user = new ApplicationUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            EmailConfirmed = true,
        };

        // Creating user
        var result = userManager
            .CreateAsync(user, "Admin@1811").Result;
        
        if (!result.Succeeded)
            throw new ApplicationException(result.Errors.First().Description);

        // Adding role
        result = userManager
            .AddToRoleAsync(user, PoliciesAndRoles.Roles.User).Result;
        if (!result.Succeeded)
            throw new ApplicationException($"Can't add player role for {user.Email}");
        
        // Adding claims
        result = userManager.AddClaimsAsync(user,
        [
            new Claim(JwtClaimTypes.Name, user.UserName),
            new Claim(JwtClaimTypes.Email, user.Email),
            new Claim(JwtClaimTypes.Role, PoliciesAndRoles.Roles.User)
        ]).Result;
        
        if (!result.Succeeded)
            throw new ApplicationException($"Can't add claims for {user.Email}");
        
        return host;
    }
}
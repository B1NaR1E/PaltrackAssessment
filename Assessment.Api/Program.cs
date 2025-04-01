using System.Reflection;
using Assessment.Api.Database;
using Assessment.Api.WebApi;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddMemoryCache();
services.AddHealthChecks();

var tokenIssuerSettings = builder.Configuration.GetSection(TokenIssuerConfig.SectionName);
services.Configure<TokenIssuerConfig>(tokenIssuerSettings);

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

var migrationsAssembly = typeof(Program)
    .GetTypeInfo().Assembly.GetName().Name;

services.AddScoped<ITokenRequester, TokenRequester>();
services.AddScoped<IIdentityManager, IdentityManager>();
services.AddTransient<IProfileService, CustomProfileService>();

services.AddDbContext<IdentityDbContext>(options =>
    options.UseNpgsql(connectionString));

services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

services.AddIdentityServer(opt =>
    {
        opt.IssuerUri = tokenIssuerSettings.GetValue<string>("Authority");
    })
    .AddDeveloperSigningCredential() // without a certificate, for dev only
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
        options.EnableTokenCleanup = true;
    })
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
    })
    .AddAspNetIdentity<ApplicationUser>()
    .AddProfileService<CustomProfileService>();

services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
    options.AddPolicy(PoliciesAndRoles.Policies.CanRead, AuthPolicyBuilder.CanRead);
});

services.AddJwtAuthentication(builder.Configuration);

services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
        }
    ));

var app = builder.Build();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllers();
app.UseHealthChecks();

app
    .MigrateDatabase()
    .CreateAdminUserIfNotExists()
    .Run();
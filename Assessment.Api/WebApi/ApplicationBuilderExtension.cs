namespace Assessment.Api.WebApi;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
        });

        return app;
    }
}


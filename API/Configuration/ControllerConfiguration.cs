namespace API.Configuration;

public static class ControllerConfiguration
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ModelValidatorProviders.Clear();
        });

        return services;
    }

    public static IApplicationBuilder UseCustomControllers(this WebApplication app)
    {
        // NOTE: Place area definition here, example:
        app.MapAreaControllerRoute("AreaCrm", "Crm", "api/crm/{controller}/{action}");

        app.MapControllers();

        return app;
    }
}

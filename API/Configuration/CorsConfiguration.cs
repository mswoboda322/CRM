namespace API.Configuration;

public static class CorsConfiguration
{
    private const string SettingsCorsPolcyName = "_cors_policy_settings";

    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsSettingsSection = configuration.GetSection(nameof(CorsSettings));
        services.Configure<CorsSettings>(corsSettingsSection);

        var corsSettings = corsSettingsSection.Get<CorsSettings>() ??
            throw new InvalidOperationException("Failed to access the CORS settings.");

        services.AddCors(options => options.AddPolicy(SettingsCorsPolcyName, builder =>
        {
            builder = builder.AllowAnyOrigin();
            builder = builder.AllowAnyHeader();
            builder = builder.AllowAnyMethod();

            //builder = (corsSettings.Origins.Any())
            //    ? builder.WithOrigins(corsSettings.Origins.ToArray())
            //    : builder.SetIsOriginAllowed(x => true);

            //builder = (corsSettings.Headers.Any())
            //    ? builder.WithHeaders(corsSettings.Headers.ToArray())
            //    : builder.AllowAnyHeader();

            //builder = (corsSettings.Methods.Any())
            //    ? builder.WithMethods(corsSettings.Methods.ToArray())
            //    : builder.AllowAnyMethod();

            //builder = (corsSettings.AllowCredentials)
            //    ? builder.AllowCredentials()
            //    : builder.DisallowCredentials();

            //if (corsSettings.AllowWildcardOrigins)
            //    builder = builder.SetIsOriginAllowedToAllowWildcardSubdomains();

            builder.Build();
        }));

        return services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors(SettingsCorsPolcyName);

        return app;
    }
}
public class CorsSettings
{
    public IList<string> Origins { get; set; } = [];
    public IList<string> Methods { get; set; } = [];
    public IList<string> Headers { get; set; } = [];
    public bool AllowCredentials { get; set; }
    public bool AllowWildcardOrigins { get; set; }
}


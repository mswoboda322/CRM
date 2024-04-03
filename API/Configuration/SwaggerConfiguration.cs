using Microsoft.OpenApi.Models;

namespace API.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(config =>
        {
            var assemblyVersion = typeof(SwaggerConfiguration)?.Assembly?.GetName()?.Version?.ToString() ?? "b.d";

            // NOTE: Place swagger area docs definition here, example:
            config.SwaggerDoc("Crm", new OpenApiInfo
            {
                Title = "CRM",
                Version = $"crm_v_{assemblyVersion}"
            });

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "Bearer",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    },
                    new[] { "readAccess", "writeAccess" }
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                // NOTE: Add swagger docs endpoint here, example:
                config.SwaggerEndpoint("/swagger/Crm/swagger.json", "Crm");
            });
        }

        return app;
    }
}

using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Infrastructure;
public static class DependencyInjection
{
    private const string LocalizationResourcesPath = "Localization";

    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettingsSection = configuration.GetSection(nameof(DatabaseSettings));
        services.Configure<DatabaseSettings>(databaseSettingsSection);

        var databaseSettings = databaseSettingsSection.Get<DatabaseSettings>();

        var connectionString = databaseSettings?.ConnectionString ??
            throw new InvalidOperationException("No connection string specified.");

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }, ServiceLifetime.Transient);

        services.AddIdentityCore<User>().AddEntityFrameworkStores<DatabaseContext>();
        

        return services;
    }

    public static IApplicationBuilder UseInfrastructureLayer(this IApplicationBuilder app, IServiceProvider service)
    {
        using var scope = service.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        var databaseSettings = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

        if (databaseSettings.AutoMigrate)
        {
            databaseContext.Database.Migrate();
        }

        return app;
    }
}

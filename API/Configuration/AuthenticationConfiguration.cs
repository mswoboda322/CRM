using Application.Features.Users.Models;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Configuration;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettingsSection = configuration.GetSection(nameof(AuthenticationSettings));
        services.Configure<AuthenticationSettings>(authenticationSettingsSection);

        var authenticationSettings = authenticationSettingsSection.Get<AuthenticationSettings>();

        services.AddIdentity<User, IdentityRole<long>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddEntityFrameworkStores<DatabaseContext>()
        .AddDefaultTokenProviders()
        .AddSignInManager();
        

        var secret = (authenticationSettings?.Secret is not null)
            ? Encoding.ASCII.GetBytes(authenticationSettings.Secret)
            : throw new InvalidOperationException("No authentication secret specified.");

        if (authenticationSettings.Development)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }
        else
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudiences = authenticationSettings.Audiences,
                        ValidIssuers = authenticationSettings.Issuers,
                    };
                });
        }

        return services;
    }
}

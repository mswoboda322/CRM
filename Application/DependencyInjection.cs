﻿using Application.Abstractions;
using Application.Features.Reports.DTOs;
using Application.Features.Tasks.DTOs;
using Application.Features.Users.DTOs;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblyOf<Assembly>()
            .AddClasses(y => y.AssignableTo<IScopedApplicationService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(x => x.FromAssemblyOf<Assembly>()
            .AddClasses(y => y.AssignableTo<ISingletonApplicationService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.AddSingleton(provider => new MapperConfiguration(mapper =>
        {
            mapper.AddProfile(new UserProfile());
            mapper.AddProfile(new ReportProfile());
            mapper.AddProfile(new TaskProfile());

        }).CreateMapper());

        return services;
    }
}

public sealed class Assembly
{
}


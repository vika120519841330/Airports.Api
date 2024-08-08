﻿using Airports.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Airports.Api.Configs;

public static class DbContextConfig
{
    public static void AddDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        var optionBuilder = (DbContextOptionsBuilder options) =>
        {
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
            options.UseSqlServer(
                configuration.GetConnectionString("AppDbContext"),
                builder =>
                {
                    builder.CommandTimeout(120);
                    builder.EnableRetryOnFailure();
                });
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };

        services.AddDbContextFactory<AppDbContext>(
            options => optionBuilder(options), ServiceLifetime.Transient);
    }
}
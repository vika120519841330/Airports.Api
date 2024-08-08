using Airports.Api.Test.TestHelpers;
using Airports.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airports.Api.Test;

public class TestDbContextFactoryHelper : FilesHelper, IDbContextFactory<AppDbContext>
{
    protected IConfigurationRoot Configuration =>
        new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

    protected IConfigurationRoot ConfigurationDevelopment =>
        new ConfigurationBuilder()
        .AddJsonFile("appsettings.Development.json", optional: false)
        .Build();

    private string ConnectionString => ConfigurationDevelopment.GetConnectionString("SpeditionDb");

    public AppDbContext CreateDbContext()
    {
        return CreateApplicationDbContext();
    }

    private AppDbContext CreateApplicationDbContext()
    {
        var services = new ServiceCollection();

        services.AddDbContextFactory<AppDbContext>(
            options =>
            {
                options.UseSqlServer(ConfigurationDevelopment.GetConnectionString("AppDbContext"), builder => builder.EnableRetryOnFailure());
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            },
            ServiceLifetime.Transient);

        var servicesProvider = services.BuildServiceProvider();
        var contextFactory = servicesProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        return contextFactory.CreateDbContext();
    }

    private async Task<AppDbContext> CreateApplicationDbContextAsync()
    {
        var services = new ServiceCollection();

        services.AddDbContextFactory<AppDbContext>(
            options =>
            {
                options.UseSqlServer(ConfigurationDevelopment.GetConnectionString("AppDbContext"), builder => builder.EnableRetryOnFailure());
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            },
            ServiceLifetime.Transient);

        var servicesProvider = services.BuildServiceProvider();
        var contextFactory = servicesProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        return await contextFactory.CreateDbContextAsync();
    }
}

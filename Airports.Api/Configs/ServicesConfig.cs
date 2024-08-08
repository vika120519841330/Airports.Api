using Airports.BusinessLayer.Services;
using Airports.Shared.Models;
using Airports.Shared.Static;
using Microsoft.AspNetCore.ResponseCompression;

namespace Airports.Api.Configs;

public static class ServicesConfig
{
    public static void AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ConfigBase>(config.GetSection("TeleportHost"));

        services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });

        services.AddHttpClient(StaticItems.airportsHttpClient).ConfigureHttpClient(x =>
        {
            x.Timeout = TimeSpan.FromMinutes(5);
        });

        services.AddTransient<HttpService>();
        services.AddTransient<AirportsService>();
    }
}
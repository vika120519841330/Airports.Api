using Airports.BusinessLayer.Services;

namespace Airports.Api.Configs;

public static class ServicesConfig
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IataService>();
    }
}
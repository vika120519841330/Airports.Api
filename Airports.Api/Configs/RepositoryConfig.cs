using Airports.DataAccess.Interfaces;
using Airports.DataAccess.Models.DbModels;
using Airports.DataAccess.Repo;

namespace Airports.Api.Configs;

public static class RepositoryConfig
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ITRepository<Country>, TRepository<Country>>(); 
        services.AddTransient<ITRepository<City>, TRepository<City>>();
        services.AddTransient<ITRepository<Location>, TRepository<Location>>();
        services.AddTransient<ITRepository<Airport>, TRepository<Airport>>();
        services.AddTransient<AppRepositories>();
    }
}
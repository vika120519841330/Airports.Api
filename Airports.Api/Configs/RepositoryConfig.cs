using Airports.DataAccess.Interfaces;
using Airports.DataAccess.Models.DbModels;
using Airports.DataAccess.Repo;

namespace Airports.Api.Configs;

public static class RepositoryConfig
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ITRepository<IataBase>, TRepository<IataBase>>();
        services.AddTransient<AppRepositories>();
    }
}
using Airports.DataAccess.Interfaces;
using Airports.DataAccess.Models.DbModels;
using Airports.DataAccess.Repo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;

namespace Airports.Api.Test.TestHelpers;

public abstract class RepositoryHelper : TestDbContextFactoryHelper
{
    protected AppRepositories GetAppRepositories()
    {
        var services = new ServiceCollection();
        var dbFactory = new TestDbContextFactoryHelper();

        services.AddSingleton<ITRepository<Location>>(prov => new TRepository<Location>(dbFactory));
        services.AddSingleton<ITRepository<Airport>>(prov => new TRepository<Airport>(dbFactory));
        services.AddSingleton<ITRepository<City>>(prov => new TRepository<City>(dbFactory));
        services.AddSingleton<ITRepository<Country>>(prov => new TRepository<Country>(dbFactory));

        var servicesProvider = services.BuildServiceProvider();
        return new AppRepositories(servicesProvider);
    }

}

using Airports.DataAccess.Interfaces;
using Airports.DataAccess.Models.DbModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Airports.DataAccess.Repo;

public class AppRepositories
{
    private readonly IServiceProvider serviceProvider;

    public AppRepositories(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ITRepository<Airport> Airport => serviceProvider?.GetService<ITRepository<Airport>>();

    public ITRepository<City> Cities => serviceProvider?.GetService<ITRepository<City>>();

    public ITRepository<Country> Countries => serviceProvider?.GetService<ITRepository<Country>>();

}
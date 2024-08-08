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

    public ITRepository<IataBase> Iatas => serviceProvider?.GetService<ITRepository<IataBase>>();

}
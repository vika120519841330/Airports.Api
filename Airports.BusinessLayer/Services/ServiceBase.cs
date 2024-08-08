using Airports.DataAccess.Repo;
using Airports.Shared.Interfaces;

namespace Airports.BusinessLayer.Services;

public abstract class ServiceBase : INotify
{
    protected readonly AppRepositories appRepositories; 
    protected readonly HttpService httpService;

    public ServiceBase(AppRepositories appRepositories, HttpService httpService)
    {
        this.appRepositories = appRepositories;
        this.httpService = httpService;
    }

    public string NotifyMessage => httpService?.NotifyMessage ?? string.Empty;
}
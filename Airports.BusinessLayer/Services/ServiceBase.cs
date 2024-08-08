using Airports.DataAccess.Repo;
using Airports.Shared.Interfaces;
using AutoMapper;

namespace Airports.BusinessLayer.Services;

public abstract class ServiceBase : INotify
{
    protected readonly IMapper mapper;
    protected readonly AppRepositories appRepositories;
    public ServiceBase(AppRepositories appRepositories, IMapper mapper)
    {
        this.appRepositories = appRepositories;
        this.mapper = mapper;
    }

    public string Message { get; set; } = string.Empty;
}
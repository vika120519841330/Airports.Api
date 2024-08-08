using Airports.DataAccess.Repo;
using AutoMapper;

namespace Airports.BusinessLayer.Services;

public class AirportsService : ServiceBase
{
    public AirportsService(AppRepositories appRepositories, IMapper mapper) : base(appRepositories, mapper)
    {
    }

    public async Task<decimal?> GetDistance(string first, string second, CancellationToken token = default)
    {
        decimal? distance = default;

        return distance;
    }
}

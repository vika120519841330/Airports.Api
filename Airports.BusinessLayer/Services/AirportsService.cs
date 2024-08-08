using Airports.BusinessLayer.Helpers;
using Airports.DataAccess.Models.DbModels;
using Airports.DataAccess.Repo;
using AutoMapper;

namespace Airports.BusinessLayer.Services;

public class AirportsService : ServiceBase
{
    public AirportsService(AppRepositories appRepositories, IMapper mapper) : base(appRepositories, mapper)
    {
    }

    public async Task<double?> GetDistance(string first, string second, CancellationToken token = default)
    {
        // 1 - Find airports coordinates
        var firstLocation = await GetAirportLocation(first);
        if (firstLocation == null) return null;

        var secondLocation = await GetAirportLocation(second);
        if(secondLocation == null) return null;

        (double lon, double lat) firstCoordinates = (firstLocation.Longitude.ConvertDegreeToRadian(), firstLocation.Lattitude.ConvertDegreeToRadian());
        (double lon, double lat) secondCoordinates = (secondLocation.Longitude.ConvertDegreeToRadian(), secondLocation.Lattitude.ConvertDegreeToRadian());

        // 2 -
        var deltaLon = (firstCoordinates.lon - secondCoordinates.lon);
        var deltaLat = (firstCoordinates.lat - secondCoordinates.lat);

        const double earthRadius = 3958.8;
        var intermediateСalculationResult = Math.Pow(Math.Sin(deltaLat / 2), 2) + Math.Cos(firstCoordinates.lat) * Math.Cos(secondCoordinates.lat) * Math.Pow(Math.Sin(deltaLon / 2), 2);
        var calculationResult = 2 * Math.Atan2(Math.Sqrt(intermediateСalculationResult), Math.Sqrt(1 - intermediateСalculationResult));
        return Math.Round(earthRadius * calculationResult, 6);
    }

    private async Task<Location> GetAirportLocation(string iata, CancellationToken token = default)
    {
        var airport = await appRepositories.Airport.FindFirstAsync((iatadb => iata.Equals(iatadb.Iata)), token);
        int? locationId = airport != null ? airport.LocationId : null;
        if (locationId == null) return null;

        return await appRepositories.Locations.GetAsync(locationId.Value, token);
    }
}
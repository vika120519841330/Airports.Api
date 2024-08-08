using Airports.DataAccess.Models.DbModels;

namespace Airports.BusinessLayer.DTO;

public class LocationInfo
{
    public LocationInfo(decimal lon, decimal lat)
    {
        Lon = lon;
        Lat = lat;
    }
    public decimal Lon{ get; set; }

    public decimal Lat { get; set; }
}

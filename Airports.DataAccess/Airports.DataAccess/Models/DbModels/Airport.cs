using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airports.DataAccess.Models.DbModels;

[Table("Airport", Schema = "geo")]
public class Airport : IataBase
{
    [Key]
    public int Id { get; set; }

    public string Icao { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; }
}

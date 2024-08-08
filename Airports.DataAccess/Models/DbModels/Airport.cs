using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airports.DataAccess.Models.DbModels;

[Table("Airport", Schema = "geo")]
public class Airport : IataBase
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("icao")]
    public string? Icao { get; set; }

    [Column("city_id")]
    public int CityId { get; set; }

    public virtual City City { get; set; }

    [Column("location_id")]
    public int LocationId { get; set; }

    public virtual Location Location { get; set; }
}

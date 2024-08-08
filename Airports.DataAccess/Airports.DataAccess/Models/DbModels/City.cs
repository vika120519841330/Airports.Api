using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airports.DataAccess.Models.DbModels;

[Table("City", Schema = "geo")]
public class City : IataBase
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("country_id")]
    public int CountryId {  get; set; }

    public virtual Country Country { get; set; }

    public virtual List<Airport> Airports { get; set; } = new();
}

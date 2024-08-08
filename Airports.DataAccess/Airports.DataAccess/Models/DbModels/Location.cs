using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Airports.DataAccess.Models.DbModels;

public class Location
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public decimal Longitude { get; set; } // от -180 до +180

    public decimal Lattitude { get; set; }// от -90 до +90

    public virtual List<Airport> Airports { get; set; }
}

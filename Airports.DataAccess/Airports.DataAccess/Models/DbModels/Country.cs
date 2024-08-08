using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airports.DataAccess.Models.DbModels;

[Table("Country", Schema = "geo")]
public class Country : IataBase
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public virtual List<City> Cities { get; set; } = new();
}

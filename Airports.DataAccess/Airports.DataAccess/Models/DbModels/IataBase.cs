using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airports.DataAccess.Models.DbModels;

public abstract class IataBase : NameBase
{
    [Column("iata")]
    public string Iata { get; set; } = string.Empty;
}

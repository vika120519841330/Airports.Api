using System.ComponentModel.DataAnnotations.Schema;

namespace Airports.DataAccess.Models.DbModels;

public abstract class NameBase
{
    [Column("name")]
    public virtual string Name { get; set; }
}
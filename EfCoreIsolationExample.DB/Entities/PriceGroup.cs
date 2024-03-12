using System;
using System.ComponentModel.DataAnnotations;

namespace EfCoreIsolationExample.DB.Entities;

public record PriceGroup
{
    [Key]
    public int ID { get; init; }
    
    public Guid PriceVersion { get; init; }
}

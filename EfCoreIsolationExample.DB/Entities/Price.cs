using System;
using System.ComponentModel.DataAnnotations;

namespace EfCoreIsolationExample.DB.Entities;

public record Price
{
    [Key]
    public int ID { get; init; }  

    public decimal Value { get; init; }

    public DateOnly Start { get; init; }
    
    public DateOnly End { get; init; }
    
    public int PriceGroupID { get; init; }

    public PriceGroup PriceGroup { get; init; } = null!;
}

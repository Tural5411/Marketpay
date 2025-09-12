using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class Product : BaseEntity
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Stock { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? Category { get; set; }

    public bool IsActive { get; set; } = true;
}


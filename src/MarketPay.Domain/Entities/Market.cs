using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class Market : BaseEntity
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
}

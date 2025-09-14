using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class Product : BaseEntity
{
    [Required]
    public Guid MarketId { get; set; }

    [Required]
    [StringLength(200)]
    public string ProductBarcode { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    public decimal ProductPrice { get; set; }

    [Required]
    [StringLength(50)]
    public string ProductUnit { get; set; } = string.Empty;

    // Navigation properties
    public Market Market { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}


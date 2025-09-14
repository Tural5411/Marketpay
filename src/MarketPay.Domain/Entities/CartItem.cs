using System.ComponentModel.DataAnnotations;

namespace MarketPay.Domain.Entities;

public class CartItem : BaseEntity
{
    [Required]
    public Guid CartId { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    // Navigation properties
    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

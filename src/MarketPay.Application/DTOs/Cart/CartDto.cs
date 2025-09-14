using MarketPay.Application.DTOs.CartItem;

namespace MarketPay.Application.DTOs.Cart;

public class CartDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid MarketId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new();
}

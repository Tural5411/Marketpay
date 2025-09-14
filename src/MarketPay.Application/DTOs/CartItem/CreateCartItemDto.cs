namespace MarketPay.Application.DTOs.CartItem;

public class CreateCartItemDto
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

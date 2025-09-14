namespace MarketPay.Application.DTOs.Cart;

public class CreateCartDto
{
    public Guid UserId { get; set; }
    public Guid MarketId { get; set; }
}

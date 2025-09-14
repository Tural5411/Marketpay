namespace MarketPay.Application.DTOs.SupportChat;

public class CreateSupportChatDto
{
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
}

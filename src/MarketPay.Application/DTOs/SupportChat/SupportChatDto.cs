namespace MarketPay.Application.DTOs.SupportChat;

public class SupportChatDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string UserFullName { get; set; } = string.Empty;
}

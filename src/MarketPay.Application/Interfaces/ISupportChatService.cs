using MarketPay.Application.DTOs.SupportChat;

namespace MarketPay.Application.Interfaces;

public interface ISupportChatService
{
    Task<IEnumerable<SupportChatDto>> GetAllAsync();
    Task<SupportChatDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<SupportChatDto>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<SupportChatDto>> GetBySenderAsync(string sender);
    Task<SupportChatDto> CreateAsync(CreateSupportChatDto createSupportChatDto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

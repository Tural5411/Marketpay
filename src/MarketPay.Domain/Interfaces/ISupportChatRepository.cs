using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface ISupportChatRepository : IRepository<SupportChat>
{
    Task<IEnumerable<SupportChat>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<SupportChat>> GetBySenderAsync(string sender);
}

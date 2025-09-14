using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<IEnumerable<Cart>> GetByUserIdAsync(Guid userId);
    Task<Cart?> GetActiveCartByUserIdAsync(Guid userId, Guid marketId);
    Task<IEnumerable<Cart>> GetByMarketIdAsync(Guid marketId);
}

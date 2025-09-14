using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface ICartItemRepository : IRepository<CartItem>
{
    Task<IEnumerable<CartItem>> GetByCartIdAsync(Guid cartId);
    Task<CartItem?> GetByCartAndProductAsync(Guid cartId, Guid productId);
}

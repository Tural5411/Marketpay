using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class CartItemRepository : Repository<CartItem>, ICartItemRepository
{
    public CartItemRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CartItem>> GetByCartIdAsync(Guid cartId)
    {
        return await _context.CartItems
            .Where(ci => ci.CartId == cartId)
            .Include(ci => ci.Product)
            .ToListAsync();
    }

    public async Task<CartItem?> GetByCartAndProductAsync(Guid cartId, Guid productId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
    }
}

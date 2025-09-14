using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Cart>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .Where(c => c.UserId == userId)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .ToListAsync();
    }

    public async Task<Cart?> GetActiveCartByUserIdAsync(Guid userId, Guid marketId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.MarketId == marketId && c.Status == "active");
    }

    public async Task<IEnumerable<Cart>> GetByMarketIdAsync(Guid marketId)
    {
        return await _context.Carts
            .Where(c => c.MarketId == marketId)
            .Include(c => c.User)
            .ToListAsync();
    }
}

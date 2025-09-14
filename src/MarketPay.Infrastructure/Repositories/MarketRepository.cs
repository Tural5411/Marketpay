using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class MarketRepository : Repository<Market>, IMarketRepository
{
    public MarketRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Market?> GetByCodeAsync(string code)
    {
        return await _context.Markets.FirstOrDefaultAsync(m => m.Code == code);
    }

    public async Task<bool> CodeExistsAsync(string code)
    {
        return await _context.Markets.AnyAsync(m => m.Code == code);
    }
}

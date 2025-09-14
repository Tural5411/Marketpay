using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class SupportChatRepository : Repository<SupportChat>, ISupportChatRepository
{
    public SupportChatRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SupportChat>> GetByUserIdAsync(Guid userId)
    {
        return await _context.SupportChats
            .Where(sc => sc.UserId == userId)
            .OrderByDescending(sc => sc.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<SupportChat>> GetBySenderAsync(string sender)
    {
        return await _context.SupportChats
            .Where(sc => sc.Sender == sender)
            .Include(sc => sc.User)
            .OrderByDescending(sc => sc.CreatedAt)
            .ToListAsync();
    }
}

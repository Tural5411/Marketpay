using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Payments
            .Where(p => p.UserId == userId)
            .Include(p => p.Cart)
            .OrderByDescending(p => p.PaidAt)
            .ToListAsync();
    }

    public async Task<Payment?> GetByCartIdAsync(Guid cartId)
    {
        return await _context.Payments
            .Include(p => p.Cart)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.CartId == cartId);
    }
}

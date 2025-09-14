using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId);
    Task<Payment?> GetByCartIdAsync(Guid cartId);
}

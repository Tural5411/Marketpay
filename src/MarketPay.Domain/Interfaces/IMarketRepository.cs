using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface IMarketRepository : IRepository<Market>
{
    Task<Market?> GetByCodeAsync(string code);
    Task<bool> CodeExistsAsync(string code);
}

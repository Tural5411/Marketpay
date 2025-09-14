using MarketPay.Domain.Common;
using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<Product>> GetByMarketIdAsync(Guid marketId);
    Task<Product?> GetByBarcodeAsync(string barcode);
    Task<bool> IsBarcodeExistsAsync(string barcode, Guid? excludeId = null);
    Task<PaginatedResult<Product>> GetActiveProductsPaginatedAsync(PaginationRequest request);
    Task<PaginatedResult<Product>> GetByMarketPaginatedAsync(Guid marketId, PaginationRequest request);
    Task<PaginatedResult<Product>> SearchProductsPaginatedAsync(string searchTerm, PaginationRequest request);
}


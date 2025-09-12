using MarketPay.Domain.Common;
using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<IEnumerable<Product>> GetProductsInStockAsync();
    Task<bool> IsProductNameExistsAsync(string name, int? excludeId = null);
    Task<PaginatedResult<Product>> GetActiveProductsPaginatedAsync(PaginationRequest request);
    Task<PaginatedResult<Product>> GetProductsByCategoryPaginatedAsync(string category, PaginationRequest request);
    Task<PaginatedResult<Product>> SearchProductsPaginatedAsync(string searchTerm, PaginationRequest request);
    Task<PaginatedResult<Product>> GetProductsWithFilterAsync(ProductPaginationRequest request);
}


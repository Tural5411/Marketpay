using MarketPay.Application.DTOs.Product;
using MarketPay.Domain.Common;

namespace MarketPay.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<PaginatedResult<ProductDto>> GetAllAsync(PaginationRequest request);
    Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
    Task<IEnumerable<ProductDto>> GetByMarketIdAsync(Guid marketId);
    Task<ProductDto?> GetByBarcodeAsync(string barcode);
    Task<PaginatedResult<ProductDto>> GetActiveProductsPaginatedAsync(PaginationRequest request);
    Task<PaginatedResult<ProductDto>> GetByMarketPaginatedAsync(Guid marketId, PaginationRequest request);
    Task<PaginatedResult<ProductDto>> SearchProductsPaginatedAsync(string searchTerm, PaginationRequest request);
    Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
    Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto updateProductDto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}


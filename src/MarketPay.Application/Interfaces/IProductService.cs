using MarketPay.Application.DTOs.Product;
using MarketPay.Domain.Common;

namespace MarketPay.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto?> GetByIdAsync(int id);
    Task<PaginatedResult<ProductDto>> GetAllAsync(PaginationRequest request);
    Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string category);
    Task<PaginatedResult<ProductDto>> GetActiveProductsPaginatedAsync(PaginationRequest request);
    Task<PaginatedResult<ProductDto>> GetProductsByCategoryPaginatedAsync(string category, PaginationRequest request);
    Task<PaginatedResult<ProductDto>> SearchProductsPaginatedAsync(string searchTerm, PaginationRequest request);
    Task<PaginatedResult<ProductDto>> GetProductsWithFilterAsync(ProductPaginationRequest request);
    Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
    Task<ProductDto> UpdateAsync(int id, UpdateProductDto updateProductDto);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}


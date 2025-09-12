using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Common;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        return await _dbSet
            .Where(p => p.Category == category && p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsInStockAsync()
    {
        return await _dbSet
            .Where(p => p.Stock > 0 && p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<bool> IsProductNameExistsAsync(string name, int? excludeId = null)
    {
        var query = _dbSet.Where(p => p.Name.ToLower() == name.ToLower());
        
        if (excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task<PaginatedResult<Product>> GetActiveProductsPaginatedAsync(PaginationRequest request)
    {
        return await GetPaginatedAsync(p => p.IsActive, request);
    }

    public async Task<PaginatedResult<Product>> GetProductsByCategoryPaginatedAsync(string category, PaginationRequest request)
    {
        return await GetPaginatedAsync(p => p.Category == category && p.IsActive, request);
    }

    public async Task<PaginatedResult<Product>> SearchProductsPaginatedAsync(string searchTerm, PaginationRequest request)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        return await GetPaginatedAsync(p => 
            p.IsActive && 
            (p.Name.ToLower().Contains(lowerSearchTerm) || 
             (p.Description != null && p.Description.ToLower().Contains(lowerSearchTerm)) ||
             (p.Category != null && p.Category.ToLower().Contains(lowerSearchTerm))), 
            request);
    }

    public async Task<PaginatedResult<Product>> GetProductsWithFilterAsync(ProductPaginationRequest request)
    {
        var query = _dbSet.AsQueryable();

        // Filtreleme
        if (request.IsActive.HasValue)
        {
            query = query.Where(p => p.IsActive == request.IsActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            query = query.Where(p => p.Category == request.Category);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var lowerSearchTerm = request.SearchTerm.ToLower();
            query = query.Where(p => 
                p.Name.ToLower().Contains(lowerSearchTerm) || 
                (p.Description != null && p.Description.ToLower().Contains(lowerSearchTerm)) ||
                (p.Category != null && p.Category.ToLower().Contains(lowerSearchTerm)));
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= request.MaxPrice.Value);
        }

        // Sıralama
        query = request.SortBy switch
        {
            ProductSortBy.Name => request.SortDirection == SortDirection.Ascending 
                ? query.OrderBy(p => p.Name) 
                : query.OrderByDescending(p => p.Name),
            ProductSortBy.Price => request.SortDirection == SortDirection.Ascending 
                ? query.OrderBy(p => p.Price) 
                : query.OrderByDescending(p => p.Price),
            ProductSortBy.Stock => request.SortDirection == SortDirection.Ascending 
                ? query.OrderBy(p => p.Stock) 
                : query.OrderByDescending(p => p.Stock),
            ProductSortBy.CreatedAt => request.SortDirection == SortDirection.Ascending 
                ? query.OrderBy(p => p.CreatedAt) 
                : query.OrderByDescending(p => p.CreatedAt),
            ProductSortBy.UpdatedAt => request.SortDirection == SortDirection.Ascending 
                ? query.OrderBy(p => p.UpdatedAt) 
                : query.OrderByDescending(p => p.UpdatedAt),
            ProductSortBy.Category => request.SortDirection == SortDirection.Ascending 
                ? query.OrderBy(p => p.Category) 
                : query.OrderByDescending(p => p.Category),
            _ => request.SortDirection == SortDirection.Ascending 
                ? query.OrderBy(p => p.Id) 
                : query.OrderByDescending(p => p.Id)
        };

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginatedResult<Product>(items, totalCount, request.PageNumber, request.PageSize);
    }
}


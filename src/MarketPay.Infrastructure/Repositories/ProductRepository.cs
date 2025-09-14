using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Common;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .Include(p => p.Market)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByMarketIdAsync(Guid marketId)
    {
        return await _dbSet
            .Where(p => p.MarketId == marketId && p.IsActive)
            .Include(p => p.Market)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }

    public async Task<Product?> GetByBarcodeAsync(string barcode)
    {
        return await _dbSet
            .Include(p => p.Market)
            .FirstOrDefaultAsync(p => p.ProductBarcode == barcode);
    }

    public async Task<bool> IsBarcodeExistsAsync(string barcode, Guid? excludeId = null)
    {
        var query = _dbSet.Where(p => p.ProductBarcode == barcode);
        
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

    public async Task<PaginatedResult<Product>> GetByMarketPaginatedAsync(Guid marketId, PaginationRequest request)
    {
        return await GetPaginatedAsync(p => p.MarketId == marketId && p.IsActive, request);
    }

    public async Task<PaginatedResult<Product>> SearchProductsPaginatedAsync(string searchTerm, PaginationRequest request)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        return await GetPaginatedAsync(p => 
            p.IsActive && 
            (p.ProductName.ToLower().Contains(lowerSearchTerm) || 
             p.ProductBarcode.ToLower().Contains(lowerSearchTerm) ||
             p.ProductUnit.ToLower().Contains(lowerSearchTerm)), 
            request);
    }
}


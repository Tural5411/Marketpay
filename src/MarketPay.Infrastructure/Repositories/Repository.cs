using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Common;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;
using MarketPay.Infrastructure.Data;

namespace MarketPay.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }

    public virtual async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public virtual async Task<PaginatedResult<T>> GetPaginatedAsync(PaginationRequest request)
    {
        var query = _dbSet.AsQueryable();
        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Id)
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginatedResult<T>(items, totalCount, request.PageNumber, request.PageSize);
    }

    public virtual async Task<PaginatedResult<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate, PaginationRequest request)
    {
        var query = _dbSet.Where(predicate);
        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Id)
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginatedResult<T>(items, totalCount, request.PageNumber, request.PageSize);
    }
}


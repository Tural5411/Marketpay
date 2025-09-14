using System.Linq.Expressions;
using MarketPay.Domain.Common;
using MarketPay.Domain.Entities;

namespace MarketPay.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(Guid id);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetQueryable();
    Task<PaginatedResult<T>> GetPaginatedAsync(PaginationRequest request);
    Task<PaginatedResult<T>> GetPaginatedAsync(Expression<Func<T, bool>> predicate, PaginationRequest request);
}


using MarketPay.Application.DTOs.Cart;

namespace MarketPay.Application.Interfaces;

public interface ICartService
{
    Task<IEnumerable<CartDto>> GetAllAsync();
    Task<CartDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<CartDto>> GetByUserIdAsync(Guid userId);
    Task<CartDto?> GetActiveCartByUserIdAsync(Guid userId, Guid marketId);
    Task<CartDto> CreateAsync(CreateCartDto createCartDto);
    Task<CartDto> CompleteCartAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

using MarketPay.Application.DTOs.CartItem;

namespace MarketPay.Application.Interfaces;

public interface ICartItemService
{
    Task<IEnumerable<CartItemDto>> GetByCartIdAsync(Guid cartId);
    Task<CartItemDto?> GetByIdAsync(Guid id);
    Task<CartItemDto> CreateAsync(CreateCartItemDto createCartItemDto);
    Task<CartItemDto> UpdateQuantityAsync(Guid id, int quantity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

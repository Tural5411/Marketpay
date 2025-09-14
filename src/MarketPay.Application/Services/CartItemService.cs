using AutoMapper;
using MarketPay.Application.DTOs.CartItem;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;

namespace MarketPay.Application.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IMapper _mapper;

    public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper)
    {
        _cartItemRepository = cartItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CartItemDto>> GetByCartIdAsync(Guid cartId)
    {
        var cartItems = await _cartItemRepository.GetByCartIdAsync(cartId);
        return _mapper.Map<IEnumerable<CartItemDto>>(cartItems);
    }

    public async Task<CartItemDto?> GetByIdAsync(Guid id)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(id);
        return cartItem == null ? null : _mapper.Map<CartItemDto>(cartItem);
    }

    public async Task<CartItemDto> CreateAsync(CreateCartItemDto createCartItemDto)
    {
        var existingItem = await _cartItemRepository.GetByCartAndProductAsync(
            createCartItemDto.CartId, createCartItemDto.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += createCartItemDto.Quantity;
            await _cartItemRepository.UpdateAsync(existingItem);
            return _mapper.Map<CartItemDto>(existingItem);
        }

        var cartItem = _mapper.Map<CartItem>(createCartItemDto);
        await _cartItemRepository.AddAsync(cartItem);
        return _mapper.Map<CartItemDto>(cartItem);
    }

    public async Task<CartItemDto> UpdateQuantityAsync(Guid id, int quantity)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(id);
        if (cartItem == null)
            throw new KeyNotFoundException("Sepet öğesi bulunamadı.");

        cartItem.Quantity = quantity;
        await _cartItemRepository.UpdateAsync(cartItem);
        return _mapper.Map<CartItemDto>(cartItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(id);
        if (cartItem == null)
            throw new KeyNotFoundException("Sepet öğesi bulunamadı.");

        await _cartItemRepository.DeleteAsync(cartItem);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _cartItemRepository.ExistsAsync(id);
    }
}

using AutoMapper;
using MarketPay.Application.DTOs.Cart;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;

namespace MarketPay.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CartDto>> GetAllAsync()
    {
        var carts = await _cartRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CartDto>>(carts);
    }

    public async Task<CartDto?> GetByIdAsync(Guid id)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        return cart == null ? null : _mapper.Map<CartDto>(cart);
    }

    public async Task<IEnumerable<CartDto>> GetByUserIdAsync(Guid userId)
    {
        var carts = await _cartRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<CartDto>>(carts);
    }

    public async Task<CartDto?> GetActiveCartByUserIdAsync(Guid userId, Guid marketId)
    {
        var cart = await _cartRepository.GetActiveCartByUserIdAsync(userId, marketId);
        return cart == null ? null : _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> CreateAsync(CreateCartDto createCartDto)
    {
        var cart = _mapper.Map<Cart>(createCartDto);
        cart.Status = "active";
        await _cartRepository.AddAsync(cart);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> CompleteCartAsync(Guid id)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        if (cart == null)
            throw new KeyNotFoundException("Sepet bulunamadı.");

        cart.Status = "completed";
        await _cartRepository.UpdateAsync(cart);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task DeleteAsync(Guid id)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        if (cart == null)
            throw new KeyNotFoundException("Sepet bulunamadı.");

        await _cartRepository.DeleteAsync(cart);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _cartRepository.ExistsAsync(id);
    }
}

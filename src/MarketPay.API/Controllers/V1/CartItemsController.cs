using Microsoft.AspNetCore.Mvc;
using MarketPay.Application.DTOs.CartItem;
using MarketPay.Application.Interfaces;

namespace MarketPay.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CartItemsController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemsController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartItemDto>> GetCartItem(Guid id)
    {
        var cartItem = await _cartItemService.GetByIdAsync(id);
        if (cartItem == null)
            return NotFound("Sepet öğesi bulunamadı");

        return Ok(cartItem);
    }

    [HttpGet("cart/{cartId}")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItemsByCart(Guid cartId)
    {
        var cartItems = await _cartItemService.GetByCartIdAsync(cartId);
        return Ok(cartItems);
    }

    [HttpPost]
    public async Task<ActionResult<CartItemDto>> CreateCartItem([FromBody] CreateCartItemDto createCartItemDto)
    {
        var cartItem = await _cartItemService.CreateAsync(createCartItemDto);
        return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
    }

    [HttpPut("{id}/quantity")]
    public async Task<ActionResult<CartItemDto>> UpdateQuantity(Guid id, [FromBody] int quantity)
    {
        try
        {
            var cartItem = await _cartItemService.UpdateQuantityAsync(id, quantity);
            return Ok(cartItem);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(Guid id)
    {
        try
        {
            await _cartItemService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

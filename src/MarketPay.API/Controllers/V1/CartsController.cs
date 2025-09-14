using Microsoft.AspNetCore.Mvc;
using MarketPay.Application.DTOs.Cart;
using MarketPay.Application.Interfaces;

namespace MarketPay.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartDto>>> GetCarts()
    {
        var carts = await _cartService.GetAllAsync();
        return Ok(carts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartDto>> GetCart(Guid id)
    {
        var cart = await _cartService.GetByIdAsync(id);
        if (cart == null)
            return NotFound("Sepet bulunamadı");

        return Ok(cart);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<CartDto>>> GetCartsByUser(Guid userId)
    {
        var carts = await _cartService.GetByUserIdAsync(userId);
        return Ok(carts);
    }

    [HttpGet("active")]
    public async Task<ActionResult<CartDto>> GetActiveCart([FromQuery] Guid userId, [FromQuery] Guid marketId)
    {
        var cart = await _cartService.GetActiveCartByUserIdAsync(userId, marketId);
        if (cart == null)
            return NotFound("Aktif sepet bulunamadı");

        return Ok(cart);
    }

    [HttpPost]
    public async Task<ActionResult<CartDto>> CreateCart([FromBody] CreateCartDto createCartDto)
    {
        var cart = await _cartService.CreateAsync(createCartDto);
        return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
    }

    [HttpPut("{id}/complete")]
    public async Task<ActionResult<CartDto>> CompleteCart(Guid id)
    {
        try
        {
            var cart = await _cartService.CompleteCartAsync(id);
            return Ok(cart);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(Guid id)
    {
        try
        {
            await _cartService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

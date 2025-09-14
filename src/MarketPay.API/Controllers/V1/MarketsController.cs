using Microsoft.AspNetCore.Mvc;
using MarketPay.Application.DTOs.Market;
using MarketPay.Application.Interfaces;

namespace MarketPay.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class MarketsController : ControllerBase
{
    private readonly IMarketService _marketService;

    public MarketsController(IMarketService marketService)
    {
        _marketService = marketService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarketDto>>> GetMarkets()
    {
        var markets = await _marketService.GetAllAsync();
        return Ok(markets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MarketDto>> GetMarket(Guid id)
    {
        var market = await _marketService.GetByIdAsync(id);
        if (market == null)
            return NotFound("Market bulunamadı");

        return Ok(market);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<MarketDto>> GetMarketByCode(string code)
    {
        var market = await _marketService.GetByCodeAsync(code);
        if (market == null)
            return NotFound("Market bulunamadı");

        return Ok(market);
    }

    [HttpPost]
    public async Task<ActionResult<MarketDto>> CreateMarket([FromBody] CreateMarketDto createMarketDto)
    {
        try
        {
            var market = await _marketService.CreateAsync(createMarketDto);
            return CreatedAtAction(nameof(GetMarket), new { id = market.Id }, market);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MarketDto>> UpdateMarket(Guid id, [FromBody] UpdateMarketDto updateMarketDto)
    {
        try
        {
            var market = await _marketService.UpdateAsync(id, updateMarketDto);
            return Ok(market);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMarket(Guid id)
    {
        try
        {
            await _marketService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

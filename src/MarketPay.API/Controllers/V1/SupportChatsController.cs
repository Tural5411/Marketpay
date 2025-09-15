using Microsoft.AspNetCore.Mvc;
using MarketPay.Application.DTOs.SupportChat;
using MarketPay.Application.Interfaces;
using Asp.Versioning;

namespace MarketPay.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class SupportChatsController : ControllerBase
{
    private readonly ISupportChatService _supportChatService;

    public SupportChatsController(ISupportChatService supportChatService)
    {
        _supportChatService = supportChatService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupportChatDto>>> GetSupportChats()
    {
        var supportChats = await _supportChatService.GetAllAsync();
        return Ok(supportChats);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupportChatDto>> GetSupportChat(Guid id)
    {
        var supportChat = await _supportChatService.GetByIdAsync(id);
        if (supportChat == null)
            return NotFound("Destek mesajı bulunamadı");

        return Ok(supportChat);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<SupportChatDto>>> GetSupportChatsByUser(Guid userId)
    {
        var supportChats = await _supportChatService.GetByUserIdAsync(userId);
        return Ok(supportChats);
    }

    [HttpGet("sender/{sender}")]
    public async Task<ActionResult<IEnumerable<SupportChatDto>>> GetSupportChatsBySender(string sender)
    {
        var supportChats = await _supportChatService.GetBySenderAsync(sender);
        return Ok(supportChats);
    }

    [HttpPost]
    public async Task<ActionResult<SupportChatDto>> CreateSupportChat([FromBody] CreateSupportChatDto createSupportChatDto)
    {
        var supportChat = await _supportChatService.CreateAsync(createSupportChatDto);
        return CreatedAtAction(nameof(GetSupportChat), new { id = supportChat.Id }, supportChat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupportChat(Guid id)
    {
        try
        {
            await _supportChatService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

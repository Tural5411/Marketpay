using AutoMapper;
using MarketPay.Application.DTOs.SupportChat;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;

namespace MarketPay.Application.Services;

public class SupportChatService : ISupportChatService
{
    private readonly ISupportChatRepository _supportChatRepository;
    private readonly IMapper _mapper;

    public SupportChatService(ISupportChatRepository supportChatRepository, IMapper mapper)
    {
        _supportChatRepository = supportChatRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SupportChatDto>> GetAllAsync()
    {
        var supportChats = await _supportChatRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SupportChatDto>>(supportChats);
    }

    public async Task<SupportChatDto?> GetByIdAsync(Guid id)
    {
        var supportChat = await _supportChatRepository.GetByIdAsync(id);
        return supportChat == null ? null : _mapper.Map<SupportChatDto>(supportChat);
    }

    public async Task<IEnumerable<SupportChatDto>> GetByUserIdAsync(Guid userId)
    {
        var supportChats = await _supportChatRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<SupportChatDto>>(supportChats);
    }

    public async Task<IEnumerable<SupportChatDto>> GetBySenderAsync(string sender)
    {
        var supportChats = await _supportChatRepository.GetBySenderAsync(sender);
        return _mapper.Map<IEnumerable<SupportChatDto>>(supportChats);
    }

    public async Task<SupportChatDto> CreateAsync(CreateSupportChatDto createSupportChatDto)
    {
        var supportChat = _mapper.Map<SupportChat>(createSupportChatDto);
        await _supportChatRepository.AddAsync(supportChat);
        return _mapper.Map<SupportChatDto>(supportChat);
    }

    public async Task DeleteAsync(Guid id)
    {
        var supportChat = await _supportChatRepository.GetByIdAsync(id);
        if (supportChat == null)
            throw new KeyNotFoundException("Destek mesajı bulunamadı.");

        await _supportChatRepository.DeleteAsync(supportChat);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _supportChatRepository.ExistsAsync(id);
    }
}

using AutoMapper;
using MarketPay.Application.DTOs.Market;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;

namespace MarketPay.Application.Services;

public class MarketService : IMarketService
{
    private readonly IMarketRepository _marketRepository;
    private readonly IMapper _mapper;

    public MarketService(IMarketRepository marketRepository, IMapper mapper)
    {
        _marketRepository = marketRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MarketDto>> GetAllAsync()
    {
        var markets = await _marketRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MarketDto>>(markets);
    }

    public async Task<MarketDto?> GetByIdAsync(Guid id)
    {
        var market = await _marketRepository.GetByIdAsync(id);
        return market == null ? null : _mapper.Map<MarketDto>(market);
    }

    public async Task<MarketDto?> GetByCodeAsync(string code)
    {
        var market = await _marketRepository.GetByCodeAsync(code);
        return market == null ? null : _mapper.Map<MarketDto>(market);
    }

    public async Task<MarketDto> CreateAsync(CreateMarketDto createMarketDto)
    {
        if (await _marketRepository.CodeExistsAsync(createMarketDto.Code))
            throw new InvalidOperationException("Bu market kodu zaten kullanımda.");

        var market = _mapper.Map<Market>(createMarketDto);
        await _marketRepository.AddAsync(market);
        return _mapper.Map<MarketDto>(market);
    }

    public async Task<MarketDto> UpdateAsync(Guid id, UpdateMarketDto updateMarketDto)
    {
        var market = await _marketRepository.GetByIdAsync(id);
        if (market == null)
            throw new KeyNotFoundException("Market bulunamadı.");

        _mapper.Map(updateMarketDto, market);
        await _marketRepository.UpdateAsync(market);
        return _mapper.Map<MarketDto>(market);
    }

    public async Task DeleteAsync(Guid id)
    {
        var market = await _marketRepository.GetByIdAsync(id);
        if (market == null)
            throw new KeyNotFoundException("Market bulunamadı.");

        await _marketRepository.DeleteAsync(market);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _marketRepository.ExistsAsync(id);
    }

    public async Task<bool> CodeExistsAsync(string code)
    {
        return await _marketRepository.CodeExistsAsync(code);
    }
}

using MarketPay.Application.DTOs.Market;

namespace MarketPay.Application.Interfaces;

public interface IMarketService
{
    Task<IEnumerable<MarketDto>> GetAllAsync();
    Task<MarketDto?> GetByIdAsync(Guid id);
    Task<MarketDto?> GetByCodeAsync(string code);
    Task<MarketDto> CreateAsync(CreateMarketDto createMarketDto);
    Task<MarketDto> UpdateAsync(Guid id, UpdateMarketDto updateMarketDto);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> CodeExistsAsync(string code);
}

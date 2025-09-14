using MarketPay.Application.DTOs.Payment;

namespace MarketPay.Application.Interfaces;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllAsync();
    Task<PaymentDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<PaymentDto>> GetByUserIdAsync(Guid userId);
    Task<PaymentDto?> GetByCartIdAsync(Guid cartId);
    Task<PaymentDto> CreateAsync(CreatePaymentDto createPaymentDto);
    Task<bool> ExistsAsync(Guid id);
}

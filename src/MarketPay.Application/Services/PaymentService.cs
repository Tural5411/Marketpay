using AutoMapper;
using MarketPay.Application.DTOs.Payment;
using MarketPay.Application.Interfaces;
using MarketPay.Domain.Entities;
using MarketPay.Domain.Interfaces;

namespace MarketPay.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync()
    {
        var payments = await _paymentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<PaymentDto?> GetByIdAsync(Guid id)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        return payment == null ? null : _mapper.Map<PaymentDto>(payment);
    }

    public async Task<IEnumerable<PaymentDto>> GetByUserIdAsync(Guid userId)
    {
        var payments = await _paymentRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }

    public async Task<PaymentDto?> GetByCartIdAsync(Guid cartId)
    {
        var payment = await _paymentRepository.GetByCartIdAsync(cartId);
        return payment == null ? null : _mapper.Map<PaymentDto>(payment);
    }

    public async Task<PaymentDto> CreateAsync(CreatePaymentDto createPaymentDto)
    {
        var existingPayment = await _paymentRepository.GetByCartIdAsync(createPaymentDto.CartId);
        if (existingPayment != null)
            throw new InvalidOperationException("Bu sepet için zaten ödeme yapılmış.");

        var payment = _mapper.Map<Payment>(createPaymentDto);
        payment.PaidAt = DateTime.UtcNow;
        await _paymentRepository.AddAsync(payment);
        return _mapper.Map<PaymentDto>(payment);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _paymentRepository.ExistsAsync(id);
    }
}

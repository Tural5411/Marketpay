using Microsoft.AspNetCore.Mvc;
using MarketPay.Application.DTOs.Payment;
using MarketPay.Application.Interfaces;

namespace MarketPay.API.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
    {
        var payments = await _paymentService.GetAllAsync();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentDto>> GetPayment(Guid id)
    {
        var payment = await _paymentService.GetByIdAsync(id);
        if (payment == null)
            return NotFound("Ödeme bulunamadı");

        return Ok(payment);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByUser(Guid userId)
    {
        var payments = await _paymentService.GetByUserIdAsync(userId);
        return Ok(payments);
    }

    [HttpGet("cart/{cartId}")]
    public async Task<ActionResult<PaymentDto>> GetPaymentByCart(Guid cartId)
    {
        var payment = await _paymentService.GetByCartIdAsync(cartId);
        if (payment == null)
            return NotFound("Ödeme bulunamadı");

        return Ok(payment);
    }

    [HttpPost]
    public async Task<ActionResult<PaymentDto>> CreatePayment([FromBody] CreatePaymentDto createPaymentDto)
    {
        try
        {
            var payment = await _paymentService.CreateAsync(createPaymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

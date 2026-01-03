using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentMethodItem>>> GetPaymentMethods()
    {
        var dto = await _paymentMethodService.GetPaymentMethods();

        var response = dto.Select(p => new PaymentMethodResponse(
            p.Id,
            p.Name));

        return Ok(response);
    }
}

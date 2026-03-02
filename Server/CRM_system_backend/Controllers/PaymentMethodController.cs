using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/payment-methods")]
public class PaymentMethodController(
    IPaymentMethodService paymentMethodService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<PaymentMethodResponse>>> GetPaymentMethods(CancellationToken ct)
    {
        var dto = await paymentMethodService.GetPaymentMethods(ct);
        var response = mapper.Map<List<PaymentMethodResponse>>(dto);

        return Ok(response);
    }
}

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/payment-methods")]
[ApiController]
public class PaymentMethodController(
    IPaymentMethodService paymentMethodService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<PaymentMethodItem>>> GetPaymentMethods(CancellationToken ct)
    {
        var dto = await paymentMethodService.GetPaymentMethods(ct);

        var response = mapper.Map<List<PaymentMethodResponse>>(dto);

        return Ok(response);
    }
}

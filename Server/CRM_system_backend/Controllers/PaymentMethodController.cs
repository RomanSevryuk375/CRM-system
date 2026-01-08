using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly IMapper _mapper;

    public PaymentMethodController(
        IPaymentMethodService paymentMethodService,
        IMapper mapper)
    {
        _paymentMethodService = paymentMethodService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PaymentMethodItem>>> GetPaymentMethods(CancellationToken ct)
    {
        var dto = await _paymentMethodService.GetPaymentMethods(ct);

        var response = _mapper.Map<List<PaymentMethodResponse>>(dto);

        return Ok(response);
    }
}

using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/order-priorities")]
public class OrderPriorityController(
    IOrderPriorityService orderPriorityService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<OrderPriorityResponse>>> GetPriorities(CancellationToken ct)
    {
        var dto = await orderPriorityService.GetPriorities(ct);

        var response = mapper.Map<List<OrderPriorityResponse>>(dto);

        return Ok(response);
    }
}

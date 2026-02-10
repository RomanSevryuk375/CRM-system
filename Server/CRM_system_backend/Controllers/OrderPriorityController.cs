using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/order-priorities")]
[ApiController]
public class OrderPriorityController(
    IOrderPriorityService orderPriorityService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<OrderPriorityItem>>> GetPriorities(CancellationToken ct)
    {
        var dto = await orderPriorityService.GetPriorities(ct);

        var response = mapper.Map<List<OrderPriorityResponse>>(dto);

        return Ok(response);
    }
}

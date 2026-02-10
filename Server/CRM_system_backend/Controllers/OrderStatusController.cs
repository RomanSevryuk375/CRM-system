using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/order-statuses")]
[ApiController]
public class OrderStatusController(
    IOrderStatusService orderStatusService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<OrderStatusItem>>> GetOrderStatuses(CancellationToken ct)
    {
        var dto = await orderStatusService.GetOrderStatuses(ct);

        var response = mapper.Map<List<OrderStatusResponse>>(dto);

        return Ok(response);
    }
}

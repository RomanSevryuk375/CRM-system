using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderStatusController : ControllerBase
{
    private readonly IOrderStatusService _orderStatusService;

    public OrderStatusController(IOrderStatusService orderStatusService)
    {
        _orderStatusService = orderStatusService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderStatusItem>>> GetOrderStatuses()
    {
        var dto = await _orderStatusService.GetOrderStatuses();

        var response = dto.Select(o => new OrderStatusResponse(
            o.id,
            o.name));

        return Ok(response);
    }
}

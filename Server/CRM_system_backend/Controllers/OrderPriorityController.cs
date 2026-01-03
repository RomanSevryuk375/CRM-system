using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderPriorityController : ControllerBase
{
    private readonly IOrderPriorityService _orderPriorityService;

    public OrderPriorityController(IOrderPriorityService orderPriorityService)
    {
        _orderPriorityService = orderPriorityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderPriorityItem>>> GetPrioritys()
    {
        var dto = await _orderPriorityService.GetPrioritys();

        var response = dto.Select(o => new OrderPriorityResponse(
            o.Id,
            o.Name));

        return Ok(response);
    }
}

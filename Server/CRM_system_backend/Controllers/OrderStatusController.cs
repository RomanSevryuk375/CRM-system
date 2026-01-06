using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderStatusController : ControllerBase
{
    private readonly IOrderStatusService _orderStatusService;
    private readonly IMapper _mapper;

    public OrderStatusController(
        IOrderStatusService orderStatusService,
        IMapper mapper)
    {
        _orderStatusService = orderStatusService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderStatusItem>>> GetOrderStatuses()
    {
        var dto = await _orderStatusService.GetOrderStatuses();

        var response = _mapper.Map<List<OrderStatusResponse>>(dto);

        return Ok(response);
    }
}

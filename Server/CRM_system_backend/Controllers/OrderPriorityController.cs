using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderPriorityController : ControllerBase
{
    private readonly IOrderPriorityService _orderPriorityService;
    private readonly IMapper _mapper;

    public OrderPriorityController(
        IOrderPriorityService orderPriorityService,
        IMapper mapper)
    {
        _orderPriorityService = orderPriorityService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderPriorityItem>>> GetPriorities(CancellationToken ct)
    {
        var dto = await _orderPriorityService.GetPriorities(ct);

        var response = _mapper.Map<List<OrderPriorityResponse>>(dto);

        return Ok(response);
    }
}

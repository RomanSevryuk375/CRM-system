using AutoMapper;
using CRM_system_backend.Contracts.WorkInOrder;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkInOrderController : ControllerBase
{
    private readonly IWorkInOrderService _workInOrderService;
    private readonly IMapper _mapper;

    public WorkInOrderController(
        IWorkInOrderService workInOrderService,
        IMapper mapper)
    {
        _workInOrderService = workInOrderService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkInOrderItem>>> GetPagedWiO([FromQuery] WorkInOrderFilter filter, CancellationToken ct)
    {
        var dto = await _workInOrderService.GetPagedWiO(filter, ct);
        var count = await _workInOrderService.GetCountWiO(filter, ct);

        var response = _mapper.Map<List<WorkInOrderResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("order/{orderId}")]
    public async Task<ActionResult<List<WorkInOrderItem>>> GetWiOByOrderId(long orderId, CancellationToken ct)
    {
        var dto = await _workInOrderService.GetWiOByOrderId(orderId, ct);

        var response = _mapper.Map<List<WorkInOrderResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateWiO([FromBody] WorkInOrderRequest request, CancellationToken ct)
    {
        var (work, errors) = WorkInOrder.Create(
            0,
            request.OrderId,
            request.JobId,
            request.WorkerId,
            request.StatusId,
            request.TimeSpent);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _workInOrderService.CreateWiO(work!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateWiO(long id, [FromBody] WorkInOrderUpdateRequest request, CancellationToken ct)
    {
        var model = new WorkInOrderUpdateModel(
            request.WorkerId,
            request.StatusId,
            request.TimeSpent);

        var Id = await _workInOrderService.UpdateWiO(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteWIO(long id, CancellationToken ct)
    {
        var Id = await _workInOrderService.DeleteWIO(id, ct);

        return Ok(Id);
    }
}

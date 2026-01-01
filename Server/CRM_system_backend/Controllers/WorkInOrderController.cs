using CRM_system_backend.Contracts.WorkInOrder;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.WorkInOrder;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkInOrderController : ControllerBase
{
    private readonly IWorkInOrderService _workInOrderService;

    public WorkInOrderController(IWorkInOrderService workInOrderService)
    {
        _workInOrderService = workInOrderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkInOrderItem>>> GetPagetWiO([FromQuery] WorkInOrderFilter filter)
    {
        var dto = await _workInOrderService.GetPagedWiO(filter);
        var count = await _workInOrderService.GetCountWiO(filter);

        var response = dto.Select(w => new WorkInOrderItem(
            w.id,
            w.orderId,
            w.job,
            w.jobId,
            w.worker,
            w.workerId,
            w.status,
            w.statusId,
            w.timeSpent));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("order/{orderId}")]
    public async Task<ActionResult<List<WorkInOrderItem>>> GetWiOByOrderId(long orderId)
    {
        var dto = await _workInOrderService.GetWiOByOrderId(orderId);

        var response = dto.Select(w => new WorkInOrderItem(
            w.id,
            w.orderId,
            w.job,
            w.jobId,
            w.worker,
            w.workerId,
            w.status,
            w.statusId,
            w.timeSpent));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateWiO([FromBody] WorkInOrderRequest request)
    {
        var (work, errors) = WorkInOrder.Create(
            0,
            request.orderId,
            request.jobId,
            request.workerId,
            request.statusId,
            request.timeSpent);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _workInOrderService.CreateWiO(work!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateWiO(long id, [FromBody] WorkInOrderUpdateRequest request)
    {
        var model = new WorkInOrderUpdateModel(
            request.workerId,
            request.statusId,
            request.timeSpent);

        var Id = await _workInOrderService.UpdateWiO(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteWIO(long id)
    {
        var Id = await _workInOrderService.DeleteWIO(id);

        return Ok(Id);
    }
}

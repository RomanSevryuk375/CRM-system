using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.WorkInOrder;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/works-in-order")]
[ApiController]
public class WorkInOrderController(
    IWorkInOrderService workInOrderService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<WorkInOrderItem>>> GetPagedWiO(
        [FromQuery] WorkInOrderFilter filter, CancellationToken ct)
    {
        var dto = await workInOrderService.GetPagedWiO(filter, ct);
        var count = await workInOrderService.GetCountWiO(filter, ct);

        var response = mapper.Map<List<WorkInOrderResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("order/{orderId}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<WorkInOrderItem>>> GetWiOByOrderId(
        long orderId, CancellationToken ct)
    {
        var dto = await workInOrderService.GetWiOByOrderId(orderId, ct);

        var response = mapper.Map<List<WorkInOrderResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> CreateWiO(
        [FromBody] WorkInOrderRequest request, CancellationToken ct)
    {
        var (work, errors) = WorkInOrder.Create(
            0,
            request.OrderId,
            request.JobId,
            request.WorkerId,
            request.StatusId,
            request.TimeSpent);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await workInOrderService.CreateWiO(work!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> UpdateWiO(
        long id, [FromBody] WorkInOrderUpdateRequest request, CancellationToken ct)
    {
        var model = new WorkInOrderUpdateModel(
            request.WorkerId,
            request.StatusId,
            request.TimeSpent);

        await workInOrderService.UpdateWiO(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> DeleteWIO(
        long id, CancellationToken ct)
    {
        await workInOrderService.DeleteWIO(id, ct);

        return NoContent();
    }
}

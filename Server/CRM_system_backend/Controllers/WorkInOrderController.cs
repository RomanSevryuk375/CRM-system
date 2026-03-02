using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.WorkInOrder;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/works-in-order")]
public class WorkInOrderController(
    IWorkInOrderService workInOrderService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<WorkInOrderResponse>>> GetPagedWiO(
        [FromQuery] WorkInOrderFilter filter, CancellationToken ct)
    {
        var dto = await workInOrderService.GetPagedWiO(filter, ct);
        var count = await workInOrderService.GetCountWiO(filter, ct);

        var response = mapper.Map<List<WorkInOrderResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<WorkInOrderResponse>> GetWiOById(
        long id, CancellationToken ct)
    {
        var dto = await workInOrderService.GetWorkInOrderById(id, ct);
        var response = mapper.Map<WorkInOrderResponse>(dto);

        return Ok(response);
    }

    [HttpGet("orders/{orderId:long}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<WorkInOrderResponse>>> GetWiOByOrderId(
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
        var createModel = mapper.Map<WorkInOrderCreateModel>(request);
        var id = await workInOrderService.CreateWiO(createModel, ct);

        var createdDto = await workInOrderService.GetWorkInOrderById(id, ct);
        var response = mapper.Map<WorkInOrderResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetWiOById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> UpdateWiO(
        long id, [FromBody] WorkInOrderUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<WorkInOrderUpdateModel>(request);
        await workInOrderService.UpdateWiO(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> DeleteWio(
        long id, CancellationToken ct)
    {
        await workInOrderService.DeleteWio(id, ct);

        return NoContent();
    }
}

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.PartSet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.PartSet;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/v1/part-sets")]
[ApiController]
public class PartSetController(
    IPartSetService partSetService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<PartSetItem>>> GetPagedPartSets(
        [FromQuery] PartSetFilter filter, CancellationToken ct)
    {
        var dto = await partSetService.GetPagedPartSets(filter, ct);
        var count = await partSetService.GetCountPartSets(filter, ct);

        var response = mapper.Map<List<PartSetResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<PartSetItem>> GetPartSetById(
        long id, CancellationToken ct)
    {
        var response = await partSetService.GetPartSetById(id, ct);

        return Ok(response);
    }

    [HttpGet("/orders/{orderId}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<PartSetItem>>> GetPartSetsByOrderId(
        long orderId, CancellationToken ct)
    {
        var dto = await partSetService.GetPartSetsByOrderId(orderId, ct);

        var response = mapper.Map<List<PartSetResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> AddToPartSet(
        [FromBody] PartSetRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<PartSetCreateModel>(request);

        var partSetId = await partSetService.AddToPartSet(createModel, ct);

        return CreatedAtAction(
            nameof(GetPartSetById),
            new { Id = partSetId },
            null);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdatePartSet(
        long id, [FromBody] PartSetUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<PartSetUpdateModel>(request);

        await partSetService.UpdatePartSet(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteFromPartSet(long id, CancellationToken ct)
    {
        await partSetService.DeleteFromPartSet(id, ct);

        return NoContent();
    }
}

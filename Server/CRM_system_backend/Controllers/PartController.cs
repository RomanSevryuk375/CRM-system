using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Part;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/v1/parts")]
[ApiController]
public class PartController(
    IPartService partService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<PartItem>>> GetPagedParts(
        [FromQuery]PartFilter filter, CancellationToken ct)
    {
        var dto = await partService.GetPagedParts(filter, ct);
        var count = await partService.GetCountParts(filter, ct);

        var response = mapper.Map<List<PartResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreatePart(
        [FromBody] PartRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<PartCreateModel>(request);

        await partService.CreatePart(createModel, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdatePart(
        long id, [FromBody] PartUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<PartUpdateModel>(request);

        await partService.UpdatePart(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePart(
        long id, CancellationToken ct)
    {
        await partService.DeletePart(id, ct);

        return NoContent();
    }
}

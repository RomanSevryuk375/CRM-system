using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Part;
using Shared.Filters;

namespace CRM_system_backend.Controllers;


[ApiController]
[Route("api/v1/parts")]
public class PartController(
    IPartService partService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<PartResponse>>> GetPagedParts(
        [FromQuery]PartFilter filter, CancellationToken ct)
    {
        var dto = await partService.GetPagedParts(filter, ct);
        var count = await partService.GetCountParts(filter, ct);

        var response = mapper.Map<List<PartResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<PartResponse>> GetPartById(
        int id, CancellationToken ct)
    {
        var dto = await partService.GetPartById(id, ct);
        var response = mapper.Map<PartResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreatePart(
        [FromBody] PartRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<PartCreateModel>(request);
        var id = await partService.CreatePart(createModel, ct);
        
        var createdDto = await partService.GetPartById(id, ct);
        var response = mapper.Map<PartResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetPartById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdatePart(
        long id, [FromBody] PartUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<PartUpdateModel>(request);
        await partService.UpdatePart(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePart(
        long id, CancellationToken ct)
    {
        await partService.DeletePart(id, ct);

        return NoContent();
    }
}

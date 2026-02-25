using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.ProjectionModels.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Position;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/positions")]
public class PositionController(
    IPositionService positionService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<PositionResponse>>> GetPagedPositions(
        [FromQuery] PositionFilter positionFilter, CancellationToken ct)
    {
        var dto = await positionService.GetPagedPositions(positionFilter, ct);
        var count = await positionService.GetCountPositions(positionFilter, ct);

        var response = mapper.Map<List<PositionResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<PositionResponse>> GetPositionById(
        long id, CancellationToken ct)
    {
        var dto = await positionService.GetPositionById(id, ct);
        var response = mapper.Map<PositionResponse>(dto);

        return Ok(response);
    }

    [HttpPost("parts")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreatePositionWithPart(
        [FromBody] PositionWithPartRequest request, CancellationToken ct)
    {
        var partCreateModel = mapper.Map<PartCreateModel>(request);
        var positionCreateModel = mapper.Map<PositionCreateModel>(request);
        var id = await positionService.CreatePositionWithPart(positionCreateModel, partCreateModel, ct);
        
        var createdDto = await positionService.GetPositionById(id, ct);
        var response = mapper.Map<PositionResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetPositionById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdatePosition(
        long id,[FromBody] PositionUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<PositionUpdateModel>(request);
        await positionService.UpdatePosition(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePosition(
        long id, CancellationToken ct)
    {
        await positionService.DeletePosition(id, ct);

        return NoContent();
    }
}

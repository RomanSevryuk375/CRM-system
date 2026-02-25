using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Acceptance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Acceptance;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/acceptances")]
public class AcceptanceController(
    IAcceptanceService acceptanceService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AcceptanceResponse>>> GetPagedAcceptance(
        [FromQuery] AcceptanceFilter filter, CancellationToken ct)
    {
        var dto = await acceptanceService.GetPagedAcceptance(filter, ct);
        var count = await acceptanceService.GetCountAcceptance(filter, ct);

        var response = mapper.Map<List<AcceptanceResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<AcceptanceResponse>> GetAcceptanceById(
        long id, CancellationToken ct)
    {
        var dto = await acceptanceService.GetAcceptanceById(id, ct);
        var response = mapper.Map<AcceptanceResponse>(dto);
        

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateAcceptance(
        [FromBody] AcceptanceRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<AcceptanceCreateModel>(request);
        var id = await acceptanceService.CreateAcceptance(createModel, ct);
        
        var createdDto = await acceptanceService.GetAcceptanceById(id, ct);
        var response = mapper.Map<AcceptanceResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetAcceptanceById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateAcceptance(
        long id, [FromBody] AcceptanceUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<AcceptanceUpdateModel>(request);

        await acceptanceService.UpdateAcceptance(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteAcceptance(
        long id, CancellationToken ct)
    {
        await acceptanceService.DeleteAcceptance(id, ct);

        return NoContent();
    }
}

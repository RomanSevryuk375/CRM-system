using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Absence;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Absence;
using Microsoft.AspNetCore.Authorization;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/v1/absences")]
[ApiController]
public class AbsenceController(
    IAbsenceService absenceService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AbsenceResponse>>> GetPagedAbsence(
        [FromQuery] AbsenceFilter filter, CancellationToken ct)
    {
        var dto = await absenceService.GetPagedAbsence(filter, ct);

        var response = mapper.Map<List<AbsenceResponse>>(dto);

        var count = await absenceService.GetCountAbsence(filter, ct);
        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<AbsenceResponse>> GetAbsenceById(
        int id, CancellationToken ct)
    {
        var dto = await absenceService.GetAbsenceById(id, ct);
        var response = mapper.Map<AbsenceResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<AbsenceResponse>> CreateAbsence(
        [FromBody] AbsenceRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<AbsenceCreateModel>(request);
        var id = await absenceService.CreateAbsence(createModel, ct);
        
        var createdDto = await absenceService.GetAbsenceById(id, ct);
        var response = mapper.Map<AbsenceResponse>(createdDto);
        
        return CreatedAtAction(
            nameof(GetAbsenceById),
            new { Id = id },
            response); 
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateAbsence(
        int id, [FromBody] AbsenceUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<AbsenceUpdateModel>(request);
        await absenceService.UpdateAbsence(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteAbsence(
        int id, CancellationToken ct)
    {
        await absenceService.DeleteAbsence(id, ct);

        return NoContent();
    }
}

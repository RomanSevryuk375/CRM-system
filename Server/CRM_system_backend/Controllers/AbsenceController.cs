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

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateAbsence(
        [FromBody] AbsenceRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<AbsenceCreateModel>(request);
        
        var id = await absenceService.CreateAbsence(createModel, ct);

        return CreatedAtAction(nameof(GetPagedAbsence), new { id }, null);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateAbsence(
        int id, [FromBody] AbsenceUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<AbsenceUpdateModel>(request);

        await absenceService.UpdateAbsence(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteAbsence(
        int id, CancellationToken ct)
    {
        await absenceService.DeleteAbsence(id, ct);

        return NoContent();
    }
}

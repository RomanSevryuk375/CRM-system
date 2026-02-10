using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Absence;
using CRMSystem.Core.Models;
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
    public async Task<ActionResult<List<AbsenceItem>>> GetPagedAbsence(
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
        var (absence, errors) = Absence.Create(
            0,
            request.WorkerId,
            request.TypeId,
            request.StartDate,
            request.EndDate);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await absenceService.CreateAbsence(absence!, ct);

        return Created();
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

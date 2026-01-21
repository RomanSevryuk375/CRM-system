using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Absence;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Absence;
using Microsoft.AspNetCore.Authorization;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AbsenceController : ControllerBase
{
    private readonly IAbsenceService _absenceService;
    private readonly IMapper _mapper;

    public AbsenceController(
        IAbsenceService absenceService,
        IMapper mapper)
    {
        _absenceService = absenceService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<AbsenceItem>>> GetPagedAbsence(
        [FromQuery] AbsenceFilter filter,
        CancellationToken ct)
    {
        var dto = await _absenceService.GetPagedAbsence(filter, ct);

        var response = _mapper.Map<List<AbsenceResponse>>(dto);

        var count = await _absenceService.GetCountAbsence(filter, ct);
        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateAbsence(
        [FromBody] AbsenceRequest request,
        CancellationToken ct)
    {
        var (absence, errors) = Absence.Create(
            0,
            request.WorkerId,
            request.TypeId,
            request.StartDate,
            request.EndDate);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _absenceService.CreateAbsence(absence!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateAbsence(
        int id, 
        [FromBody] AbsenceUpdateRequest request,
        CancellationToken ct)
    {
        var model = _mapper.Map<AbsenceUpdateModel>(request);

        var Id = await _absenceService.UpdateAbsence(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteAbsence(
        int id,
        CancellationToken ct)
    {
        var Id = await _absenceService.DeleteAbsence(id, ct);

        return Ok(Id);
    }
}

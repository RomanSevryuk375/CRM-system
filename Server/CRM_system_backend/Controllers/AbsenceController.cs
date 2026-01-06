using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Absence;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using CRM_system_backend.Contracts.Absence;

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
    public async Task<ActionResult<List<AbsenceItem>>> GetPagedAbsence(
        [FromQuery] AbsenceFilter filter)
    {
        var dto = await _absenceService.GetPagedAbsence(filter);

        var response = _mapper.Map<List<AbsenceResponse>>(dto);

        var count = await _absenceService.GetCountAbsence(filter);
        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAbsence([FromBody] AbsenceRequest request)
    {
        var (absence, errors) = Absence.Create(
            0,
            request.WorkerId,
            request.TypeId,
            request.StartDate,
            request.EndDate);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _absenceService.CreateAbsence(absence!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateAbsence(int id, [FromBody] AbsenceUpdateRequest request)
    {
        var model = _mapper.Map<AbsenceUpdateModel>(request);

        var Id = await _absenceService.UpdateAbsence(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteAbsence(int id)
    {
        var Id = await _absenceService.DeleteAbsence(id);

        return Ok(Id);
    }
}

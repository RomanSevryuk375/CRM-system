using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Schedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Schedule;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;
    private readonly IMapper _mapper;

    public ScheduleController(
        IScheduleService scheduleService,
        IMapper mapper)
    {
        _scheduleService = scheduleService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<ScheduleItem>>> GetPagedSchedules([FromQuery] ScheduleFilter filter, CancellationToken ct)
    {
        var dto = await _scheduleService.GetPagedSchedules(filter, ct);
        var count = await _scheduleService.GetCountSchedules(filter, ct);

        var responce = _mapper.Map<List<ScheduleResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(responce);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateSchedule([FromBody]ScheduleRequest request, CancellationToken ct)
    {
        var (schedule, errors) = Schedule.Create(
            0,
            request.WorkerId,
            request.ShiftId,
            request.DateTime);

        if(errors is not null &&  errors.Any()) 
            return BadRequest(errors);

        var Id = await _scheduleService.CreateSchedule(schedule!, ct);

        return Ok(Id);
    }

    [HttpPost("with-shift")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateWithShift([FromBody] ScheduleWithShiftRequest request, CancellationToken ct)
    {
        var(schedule, errorsSchedule) = Schedule.Create(
            0,
            request.WorkerId,
            0,
            request.DateTime);

        if (errorsSchedule is not null && errorsSchedule.Any())
            return BadRequest(errorsSchedule);

        var (shift, errorsShift) = Shift.Create(
            0,
            request.Name,
            request.StartAt,
            request.EndAt);

        if (errorsShift is not null && errorsShift.Any())
            return BadRequest(errorsShift);

        var Id = await _scheduleService.CreateWithShift(schedule!, shift!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateSchedule(int id, ScheduleUpdateRequest request, CancellationToken ct)
    {
        var model = new ScheduleUpdateModel(
            request.ShiftId,
            request.DateTime);

        var Id = await _scheduleService.UpdateSchedule(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteSchedule(int id, CancellationToken ct)
    {
        var Id = await _scheduleService.DeleteSchedule(id, ct);

        return Ok(Id);
    }

}

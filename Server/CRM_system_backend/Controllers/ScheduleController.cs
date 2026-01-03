using CRM_system_backend.Contracts.Schedule;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Schedule;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ScheduleItem>>> GetPagedSchedules([FromQuery] ScheduleFilter filter)
    {
        var dto = await _scheduleService.GetPagedSchedules(filter);
        var count = await _scheduleService.GetCountSchedules(filter);

        var responce = dto.Select(s => new ScheduleResponse(
            s.Id,
            s.Worker,
            s.WorkerId,
            s.Shift,
            s.ShiftId,
            s.DateTime));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(responce);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateSchedule([FromBody]ScheduleRequest request)
    {
        var (schedule, errors) = Schedule.Create(
            0,
            request.WorkerId,
            request.ShiftId,
            request.DateTime);

        if(errors is not null &&  errors.Any()) 
            return BadRequest(errors);

        var Id = await _scheduleService.CreateSchedule(schedule!);

        return Ok(Id);
    }

    [HttpPost("with-shift")]
    public async Task<ActionResult<int>> CreateWithShift([FromBody] ScheduleWithShiftRequest request)
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

        var Id = await _scheduleService.CreateWithShift(schedule!, shift!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateSchedule(int id, ScheduleUpdateRequest request)
    {
        var model = new ScheduleUpdateModel(
            request.ShiftId,
            request.DateTime);

        var Id = await _scheduleService.UpdateSchedule(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteSchedule(int id)
    {
        var Id = await _scheduleService.DeleteSchedule(id);

        return Ok(Id);
    }

}

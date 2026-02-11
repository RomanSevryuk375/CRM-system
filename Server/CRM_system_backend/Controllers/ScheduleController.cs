using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Schedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Schedule;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/v1/schedules")]
[ApiController]
public class ScheduleController(
    IScheduleService scheduleService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<ScheduleResponse>>> GetPagedSchedules(
        [FromQuery] ScheduleFilter filter, CancellationToken ct)
    {
        var dto = await scheduleService.GetPagedSchedules(filter, ct);
        var count = await scheduleService.GetCountSchedules(filter, ct);

        var responce = mapper.Map<List<ScheduleResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(responce);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateSchedule(
        [FromBody]ScheduleRequest request, CancellationToken ct)
    {
        var (schedule, errors) = Schedule.Create(
            0,
            request.WorkerId,
            request.ShiftId,
            request.DateTime);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await scheduleService.CreateSchedule(schedule!, ct);

        return Created();
    }

    [HttpPost("with-shift")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateWithShift(
        [FromBody] ScheduleWithShiftRequest request, CancellationToken ct)
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
        {
            return BadRequest(errorsShift);
        }

        await scheduleService.CreateWithShift(schedule!, shift!, ct);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateSchedule(
        int id, ScheduleUpdateRequest request, CancellationToken ct)
    {
        var model = new ScheduleUpdateModel(
            request.ShiftId,
            request.DateTime);

        await scheduleService.UpdateSchedule(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSchedule
        (int id, CancellationToken ct)
    {
        await scheduleService.DeleteSchedule(id, ct);

        return NoContent();
    }

}

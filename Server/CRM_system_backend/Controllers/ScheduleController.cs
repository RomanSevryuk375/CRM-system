using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.ProjectionModels.Shift;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Schedule;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/schedules")]
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

        var response = mapper.Map<List<ScheduleResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<ScheduleResponse>> GetScheduleById(
        int id, CancellationToken ct)
    {
        var dto = await scheduleService.GetScheduleById(id, ct);
        var response = mapper.Map<ScheduleResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateSchedule(
        [FromBody]ScheduleRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<ScheduleCreateModel>(request);
        var id = await scheduleService.CreateSchedule(createModel, ct);
        
        var createdDto = await scheduleService.GetScheduleById(id, ct);
        var response = mapper.Map<ScheduleResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetScheduleById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPost("with-shift")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateWithShift(
        [FromBody] ScheduleWithShiftRequest request, CancellationToken ct)
    {
        var shiftCreateModel = mapper.Map<ShiftCreateModel>(request);
        var scheduleCreateModel = mapper.Map<ScheduleCreateModel>(request);
        var id = await scheduleService.CreateWithShift(scheduleCreateModel, shiftCreateModel!, ct);

        var createdDto = await scheduleService.GetScheduleById(id, ct);
        var response = mapper.Map<ScheduleResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetScheduleById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateSchedule(
        int id, ScheduleUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<ScheduleUpdateModel>(request);
        await scheduleService.UpdateSchedule(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSchedule
        (int id, CancellationToken ct)
    {
        await scheduleService.DeleteSchedule(id, ct);

        return NoContent();
    }

}

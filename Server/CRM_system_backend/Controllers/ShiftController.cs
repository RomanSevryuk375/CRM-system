using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Shift;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Shift;

namespace CRM_system_backend.Controllers;

[Route("api/v1/shifts")]
[ApiController]
public class ShiftController(
    IShiftService shiftService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<ShiftItem>>> GetShifts(CancellationToken ct)
    {
        var dto = await shiftService.GetShifts(ct);

        var response = mapper.Map<List<ShiftResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateShift(
        [FromBody] ShiftRequest request, CancellationToken ct)
    {
        var (shift, errors) = Shift.Create(
            0,
            request.Name,
            request.StartAt,
            request.EndAt);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await shiftService.CreateShift(shift!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateShift(
        int id, [FromBody]ShiftUpdateRequest request, CancellationToken ct)
    {
        var model = new ShiftUpdateModel(
            request.Name,
            request.StartAt,
            request.EndAt);

        await shiftService.UpdateShift(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteShift(int id, CancellationToken ct)
    {
        await shiftService.DeleteShift(id, ct);

        return NoContent();
    }
}

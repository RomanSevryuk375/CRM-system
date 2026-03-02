using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Shift;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Shift;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/shifts")]
public class ShiftController(
    IShiftService shiftService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<ShiftResponse>>> GetShifts(CancellationToken ct)
    {
        var dto = await shiftService.GetShifts(ct);
        var response = mapper.Map<List<ShiftResponse>>(dto);

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<ShiftResponse>> GetShiftById(int id, CancellationToken ct)
    {
        var dto = await shiftService.GetShiftById(id, ct);
        var response = mapper.Map<ShiftResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateShift(
        [FromBody] ShiftRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<ShiftCreateModel>(request);
        var id = await shiftService.CreateShift(createModel, ct);
        
        var createdDto = await shiftService.GetShiftById(id, ct);
        var response = mapper.Map<ShiftResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetShiftById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateShift(
        int id, [FromBody]ShiftUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<ShiftUpdateModel>(request);
        await shiftService.UpdateShift(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteShift(int id, CancellationToken ct)
    {
        await shiftService.DeleteShift(id, ct);

        return NoContent();
    }
}

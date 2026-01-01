using CRM_system_backend.Contracts.Shift;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Shift;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ShiftItem>>> GetShifts()
    {
        var dto = await _shiftService.GetShifts();

        var response = dto.Select(s => new ShiftResponse(
            s.id,
            s.name,
            s.startAt,
            s.endAt));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateShift([FromBody] ShiftRequest request)
    {
        var (shift, errors) = Shift.Create(
            0,
            request.name,
            request.startAt,
            request.endAt);

        if(errors is not null && errors.Any()) 
            return BadRequest(errors);

        var Id = await _shiftService.CreateShift(shift!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateShift(int id, [FromBody]ShiftUpdateRequest request)
    {
        var model = new ShiftUpdateModel(
            request.name,
            request.startAt,
            request.endAt);

        var Id = await _shiftService.UpdateShift(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteShift(int id)
    {
        var Id = await _shiftService.DeleteShift(id);

        return Ok(Id);
    }
}

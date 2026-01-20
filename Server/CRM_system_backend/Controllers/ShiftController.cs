using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Shift;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Shift;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;
    private readonly IMapper _mapper;

    public ShiftController(
        IShiftService shiftService,
        IMapper mapper)
    {
        _shiftService = shiftService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ShiftItem>>> GetShifts(CancellationToken ct)
    {
        var dto = await _shiftService.GetShifts(ct);

        var response = _mapper.Map<List<ShiftResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateShift([FromBody] ShiftRequest request, CancellationToken ct)
    {
        var (shift, errors) = Shift.Create(
            0,
            request.Name,
            request.StartAt,
            request.EndAt);

        if(errors is not null && errors.Any()) 
            return BadRequest(errors);

        var Id = await _shiftService.CreateShift(shift!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateShift(int id, [FromBody]ShiftUpdateRequest request, CancellationToken ct)
    {
        var model = new ShiftUpdateModel(
            request.Name,
            request.StartAt,
            request.EndAt);

        var Id = await _shiftService.UpdateShift(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteShift(int id, CancellationToken ct)
    {
        var Id = await _shiftService.DeleteShift(id, ct);

        return Ok(Id);
    }
}

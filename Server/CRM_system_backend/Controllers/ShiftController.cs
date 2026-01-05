using AutoMapper;
using CRM_system_backend.Contracts.Shift;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Shift;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<ShiftItem>>> GetShifts()
    {
        var dto = await _shiftService.GetShifts();

        var response = _mapper.Map<List<ShiftResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateShift([FromBody] ShiftRequest request)
    {
        var (shift, errors) = Shift.Create(
            0,
            request.Name,
            request.StartAt,
            request.EndAt);

        if(errors is not null && errors.Any()) 
            return BadRequest(errors);

        var Id = await _shiftService.CreateShift(shift!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateShift(int id, [FromBody]ShiftUpdateRequest request)
    {
        var model = new ShiftUpdateModel(
            request.Name,
            request.StartAt,
            request.EndAt);

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

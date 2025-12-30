using CRM_system_backend.Contracts.AbsenceType;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbsenceTypeController : ControllerBase
{
    private readonly IAbsenceTypeService _absenceTypeService;

    public AbsenceTypeController(
        IAbsenceTypeService absenceTypeService)
    {
        _absenceTypeService = absenceTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AbsenceTypeItem>>> GetAllAbsenceType()
    {
        var dto = await _absenceTypeService.GetAllAbsenceType();

        var response = dto.Select(a => new AbsenceTypeResponse(
            a.id,
            a.name));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CretaeAbsenceType([FromBody] AbsenceTypeRequest request)
    {
        var (absenceType, errors) = AbsenceType.Create(
            0,
            request.name);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _absenceTypeService.CretaeAbsenceType(absenceType!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateAbsenceType(int id, [FromBody] AbsenceTypeUpdateRequest request)
    {
        var Id = await _absenceTypeService.UpdateAbsenceType(id, request.name);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteAbsenceType(int id)
    {
        var Id = await _absenceTypeService.DeleteAbsenceType(id);

        return Ok(Id);
    }
}

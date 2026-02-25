using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.AbsenceType;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/absence-types")]
public class AbsenceTypeController(
    IAbsenceTypeService absenceTypeService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<AbsenceTypeResponse>>> GetAllAbsenceType(
        CancellationToken ct)
    {
        var dto = await absenceTypeService.GetAllAbsenceType(ct);

        var response = mapper.Map<List<AbsenceTypeResponse>>(dto);

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<AbsenceTypeResponse>> GetAbsenceTypeById(
        int id, CancellationToken ct)
    {
        var dto = await absenceTypeService.GetAbsenceTypeById(id, ct);
        var response = mapper.Map<AbsenceTypeResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateAbsenceType(
        [FromBody] AbsenceTypeRequest request, CancellationToken ct)
    {
        var (absenceType, errors) = AbsenceType.Create(0, request.Name);
        if (errors is not null && errors.Any()) return BadRequest(errors);
        var id = await absenceTypeService.CreateAbsenceType(absenceType!, ct);
        
        var createdDto = await absenceTypeService.GetAbsenceTypeById(id, ct);
        var response = mapper.Map<AbsenceTypeResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetAbsenceTypeById),
            new { Id = id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateAbsenceType(
        int id, [FromBody] AbsenceTypeUpdateRequest request, CancellationToken ct)
    {
        await absenceTypeService.UpdateAbsenceType(id, request.Name, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteAbsenceType(
        int id, CancellationToken ct)
    {
        await absenceTypeService.DeleteAbsenceType(id, ct);

        return NoContent();
    }
}

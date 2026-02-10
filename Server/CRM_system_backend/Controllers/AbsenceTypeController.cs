using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.AbsenceType;
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
    public async Task<ActionResult<List<AbsenceTypeItem>>> GetAllAbsenceType(
        CancellationToken ct)
    {
        var dto = await absenceTypeService.GetAllAbsenceType(ct);

        var response = mapper.Map<List<AbsenceTypeResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateAbsenceType(
        [FromBody] AbsenceTypeRequest request, CancellationToken ct)
    {
        var (absenceType, errors) = AbsenceType.Create(
            0,
            request.Name);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await absenceTypeService.CreateAbsenceType(absenceType!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateAbsenceType(
        int id, [FromBody] AbsenceTypeUpdateRequest request, CancellationToken ct)
    {
        await absenceTypeService.UpdateAbsenceType(id, request.Name, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteAbsenceType(
        int id, CancellationToken ct)
    {
        await absenceTypeService.DeleteAbsenceType(id, ct);

        return NoContent();
    }
}

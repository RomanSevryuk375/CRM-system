using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.AbsenceType;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbsenceTypeController : ControllerBase
{
    private readonly IAbsenceTypeService _absenceTypeService;
    private readonly IMapper _mapper;

    public AbsenceTypeController(
        IAbsenceTypeService absenceTypeService,
        IMapper mapper)
    {
        _absenceTypeService = absenceTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<AbsenceTypeItem>>> GetAllAbsenceType(CancellationToken ct)
    {
        var dto = await _absenceTypeService.GetAllAbsenceType(ct);

        var response = _mapper.Map<List<AbsenceTypeResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateAbsenceType([FromBody] AbsenceTypeRequest request, CancellationToken ct)
    {
        var (absenceType, errors) = AbsenceType.Create(
            0,
            request.Name);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _absenceTypeService.CreateAbsenceType(absenceType!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateAbsenceType(int id, [FromBody] AbsenceTypeUpdateRequest request, CancellationToken ct)
    {
        var Id = await _absenceTypeService.UpdateAbsenceType(id, request.Name, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteAbsenceType(int id, CancellationToken ct)
    {
        var Id = await _absenceTypeService.DeleteAbsenceType(id, ct);

        return Ok(Id);
    }
}

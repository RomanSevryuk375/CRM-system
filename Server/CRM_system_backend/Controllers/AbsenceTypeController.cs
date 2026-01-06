using AutoMapper;
using CRM_system_backend.Contracts.AbsenceType;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<AbsenceTypeItem>>> GetAllAbsenceType()
    {
        var dto = await _absenceTypeService.GetAllAbsenceType();

        var response = _mapper.Map<List<AbsenceTypeResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAbsenceType([FromBody] AbsenceTypeRequest request)
    {
        var (absenceType, errors) = AbsenceType.Create(
            0,
            request.Name);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _absenceTypeService.CreateAbsenceType(absenceType!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateAbsenceType(int id, [FromBody] AbsenceTypeUpdateRequest request)
    {
        var Id = await _absenceTypeService.UpdateAbsenceType(id, request.Name);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteAbsenceType(int id)
    {
        var Id = await _absenceTypeService.DeleteAbsenceType(id);

        return Ok(Id);
    }
}

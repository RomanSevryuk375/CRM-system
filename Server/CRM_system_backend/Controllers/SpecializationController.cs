using CRM_system_backend.Contracts;
using CRM_system_backend.Contracts.Specialization;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SpecializationItem>>> GetSpecialization()
    {
        var specializations = await _specializationService.GetSpecializations();

        var response = specializations
            .Select(s => new SpecializationResponse(
                s.Id,
                s.Name));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateSpecialization([FromBody]SpecializationRequest request)
    {
        var (specialization, errors) = Specialization.Create(
            0,
            request.Name);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _specializationService.CreateSpecialization(specialization!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateSpecialization([FromBody] SpecializationUpdateRequest request, int id)
    {
        var Id = await _specializationService.UpdateSpecialization(id, request.Name);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteSpecialization(int id)
    {
        var Id = await _specializationService.DeleteSpecialization(id);

        return Ok(Id);
    }
}

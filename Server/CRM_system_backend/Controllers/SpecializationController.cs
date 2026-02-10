using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Specialization;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/specializations")]

public class SpecializationController(
    ISpecializationService specializationService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<SpecializationItem>>> GetSpecialization(CancellationToken ct)
    {
        var dto = await specializationService.GetSpecializations(ct);

        var response = mapper.Map<List<SpecializationResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateSpecialization(
        [FromBody]SpecializationRequest request, CancellationToken ct)
    {
        var (specialization, errors) = Specialization.Create(
            0,
            request.Name);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await specializationService.CreateSpecialization(specialization!, ct);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateSpecialization(
        [FromBody] SpecializationUpdateRequest request, int id, CancellationToken ct)
    {
        await specializationService.UpdateSpecialization(id, request.Name, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSpecialization(
        int id, CancellationToken ct)
    {
        await specializationService.DeleteSpecialization(id, ct);

        return NoContent();
    }
}

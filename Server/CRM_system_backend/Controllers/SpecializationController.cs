using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Specialization;
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
    public async Task<ActionResult<List<SpecializationResponse>>> GetSpecialization(CancellationToken ct)
    {
        var dto = await specializationService.GetSpecializations(ct);
        var response = mapper.Map<List<SpecializationResponse>>(dto);

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<SpecializationResponse>> GetSpecializationById
        (int id, CancellationToken ct)
    {
        var dto = await specializationService.GetSpecializationById(id, ct);
        var response = mapper.Map<SpecializationResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateSpecialization(
        [FromBody]SpecializationRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<SpecializationCreateModel>(request);
        var id = await specializationService.CreateSpecialization(createModel, ct);
        
        var createdDto = specializationService.GetSpecializationById(id, ct);
        var response = mapper.Map<SpecializationResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetSpecializationById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateSpecialization(
        [FromBody] SpecializationUpdateRequest request, int id, CancellationToken ct)
    {
        await specializationService.UpdateSpecialization(id, request.Name, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSpecialization(
        int id, CancellationToken ct)
    {
        await specializationService.DeleteSpecialization(id, ct);

        return NoContent();
    }
}

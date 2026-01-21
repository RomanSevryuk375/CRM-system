using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Specialization;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _specializationService;
    private readonly IMapper _mapper;

    public SpecializationController(
        ISpecializationService specializationService,
        IMapper mapper)
    {
        _specializationService = specializationService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<SpecializationItem>>> GetSpecialization(CancellationToken ct)
    {
        var dto = await _specializationService.GetSpecializations(ct);

        var response = _mapper.Map<List<SpecializationResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateSpecialization([FromBody]SpecializationRequest request, CancellationToken ct)
    {
        var (specialization, errors) = Specialization.Create(
            0,
            request.Name);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _specializationService.CreateSpecialization(specialization!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateSpecialization([FromBody] SpecializationUpdateRequest request, int id, CancellationToken ct)
    {
        var Id = await _specializationService.UpdateSpecialization(id, request.Name, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteSpecialization(int id, CancellationToken ct)
    {
        var Id = await _specializationService.DeleteSpecialization(id, ct);

        return Ok(Id);
    }
}

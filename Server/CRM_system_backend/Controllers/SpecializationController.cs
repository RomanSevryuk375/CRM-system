using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]

public class SpecializationController : Controller
{
    private readonly ISpecializationService _specializationService;

    public SpecializationController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<Specialization>> GetSpecialization(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var specializations = await _specializationService.GetPagedSpecialization(page, limit);
        var totalCount = _specializationService.GetCountSpecialization();

        var response = specializations
            .Select(s => new SpecializationResponse(
                s.Id, s.Name)).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateSpecialization([FromBody]SpecializationRequest specializationRequest)
    {
        var (specialization, error) = Specialization.Create(
            0,
            specializationRequest.Name);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var specializationId = await _specializationService.CreateSpecialization(specialization);

        return Ok(specializationId);
    }

    [HttpPut("${id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateSpecialization([FromBody] SpecializationRequest specializationRequest, int id)
    {
        var result = await _specializationService.UpdateSpecialization(id, specializationRequest.Name);

        return Ok(result);
    }

    [HttpDelete]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteSpecialization(int id)
    {
        var result = await _specializationService.DeleteSpecialization(id);

        return Ok(result);
    }
}

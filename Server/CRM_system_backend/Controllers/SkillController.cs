using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Skill;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/skills")]
public class SkillController(
    ISkillService skillService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<SkillResponse>>> GetPagedSkills(
        [FromQuery] SkillFilter filter, CancellationToken ct)
    {
        var dto = await skillService.GetSkills(filter, ct);
        var count = await skillService.GetSkillsCount(filter, ct);

        var response = mapper.Map<List<SkillResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<SkillResponse>> GetSkillById(
        int id, CancellationToken ct)
    {
        var dto = await skillService.GetSkillById(id, ct);
        var response = mapper.Map<SkillResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<SkillResponse>> CreateSkill(
        [FromBody] SkillRequest request, CancellationToken ct)
    {
        var (skill, errors) = Skill.Create(0, request.WorkerId, request.SpecializationId);
        if (errors.Any()) return BadRequest(new { errors });
        var id = await skillService.CreateSkill(skill!, ct);
        
        var createdDto = await skillService.GetSkillById(id, ct);
        var response = mapper.Map<SkillResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetSkillById),
            new { id = response.Id },
            response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteSkill(
        int id, CancellationToken ct)
    {
        await skillService.DeleteSkill(id, ct);

        return NoContent();
    }
}
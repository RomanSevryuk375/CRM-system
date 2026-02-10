using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Part;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/v1/parts")]
[ApiController]
public class PartController(
    IPartService partService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<PartItem>>> GetPagedParts(
        [FromQuery]PartFilter filter, CancellationToken ct)
    {
        var dto = await partService.GetPagedParts(filter, ct);
        var count = await partService.GetCountParts(filter, ct);

        var response = mapper.Map<List<PartResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreatePart(
        [FromBody] PartRequest request, CancellationToken ct)
    {
        var (part, errors) = Part.Create(
            0,
            request.CategoryId,
            request.OemArticle,
            request.ManufacturerArticle,
            request.InternalArticle,
            request.Description,
            request.Name,
            request.Manufacturer,
            request.Applicability);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        var Id = await partService.CreatePart(part!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdatePart(
        long id, [FromBody] PartUpdateRequest request, CancellationToken ct)
    {
        var model = new PartUpdateModel(
            request.OemArticle,
            request.ManufacturerArticle,
            request.InternalArticle,
            request.Description,
            request.Name,
            request.Manufacturer,
            request.Applicability);

        await partService.UpdatePart(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePart(
        long id, CancellationToken ct)
    {
        await partService.DeletePart(id, ct);

        return NoContent();
    }
}

using CRM_system_backend.Contracts.Part;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.DTOs.Part;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartController : ControllerBase
{
    private readonly IPartService _partService;

    public PartController(IPartService partService)
    {
        _partService = partService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PartItem>>> GetPagedParts([FromQuery]PartFilter filter)
    {
        var dto = await _partService.GetPagedParts(filter);
        var count = await _partService.GetCountParts(filter);

        var response = dto.Select(p => new PartResponse(
            p.Id,
            p.Category,
            p.CategoryId,
            p.OemArticle,
            p.Manufacturer,
            p.InternalArticle,
            p.Description,
            p.Name,
            p.Manufacturer,
            p.Applicability));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreatePart([FromBody] PartRequest request)
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

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _partService.CreatePart(part!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdatePart(long id, [FromBody] PartUpdateRequest request)
    {
        var model = new PartUpdateModel(
            request.OemArticle,
            request.ManufacturerArticle,
            request.InternalArticle,
            request.Description,
            request.Name,
            request.manufacturer,
            request.Applicability);

        var Id = await _partService.UpdatePart(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeletePart(long id)
    {
        var Id = await _partService.DeletePart(id);

        return Ok(Id);
    }
}

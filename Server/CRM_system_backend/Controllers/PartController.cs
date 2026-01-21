using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Part;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartController : ControllerBase
{
    private readonly IPartService _partService;
    private readonly IMapper _mapper;

    public PartController(
        IPartService partService,
        IMapper mapper)
    {
        _partService = partService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<PartItem>>> GetPagedParts([FromQuery]PartFilter filter, CancellationToken ct)
    {
        var dto = await _partService.GetPagedParts(filter, ct);
        var count = await _partService.GetCountParts(filter, ct);

        var response = _mapper.Map<List<PartResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> CreatePart([FromBody] PartRequest request, CancellationToken ct)
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

        var Id = await _partService.CreatePart(part!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> UpdatePart(long id, [FromBody] PartUpdateRequest request, CancellationToken ct)
    {
        var model = new PartUpdateModel(
            request.OemArticle,
            request.ManufacturerArticle,
            request.InternalArticle,
            request.Description,
            request.Name,
            request.Manufacturer,
            request.Applicability);

        var Id = await _partService.UpdatePart(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeletePart(long id, CancellationToken ct)
    {
        var Id = await _partService.DeletePart(id, ct);

        return Ok(Id);
    }
}

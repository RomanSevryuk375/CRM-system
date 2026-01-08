using AutoMapper;
using CRM_system_backend.Contracts.PartCategory;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartCategoryController : ControllerBase
{
    private readonly IPartCategoryService _partCategoryService;
    private readonly IMapper _mapper;

    public PartCategoryController(
        IPartCategoryService partCategoryService,
        IMapper mapper)
    {
        _partCategoryService = partCategoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PartCategoryItem>>> GetPartCategories(CancellationToken ct)
    {
        var dto = await _partCategoryService.GetPartCategories(ct);

        var response = _mapper.Map<PartCategoryResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreatePartCategory([FromBody] PartCategoryRequest request, CancellationToken ct)
    {
        var (partCategory, errors) = PartCategory.Create(
            0,
            request.Name,
            request.Description);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _partCategoryService.CreatePartCategory(partCategory!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdatePartCategory(int id, [FromBody]PartCategoryUpdateRequest request, CancellationToken ct)
    {
        var model = new PartCategoryUpdateModel(
            request.Name,
            request.Description);

        var Id = await _partCategoryService.UpdatePartCategory(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeletePartCategory(int id, CancellationToken ct)
    {
        var Id = await _partCategoryService.DeletePartCategory(id, ct);

        return Ok(Id);
    }
}


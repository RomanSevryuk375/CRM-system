using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.PartCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.PartCategory;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/part-categories")]
public class PartCategoryController(
    IPartCategoryService partCategoryService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<PartCategoryResponse>>> GetPartCategories(CancellationToken ct)
    {
        var dto = await partCategoryService.GetPartCategories(ct);
        var response = mapper.Map<List<PartCategoryResponse>>(dto);

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<PartCategoryResponse>> GetPartCategoryById(int id, CancellationToken ct)
    {
        var dto = await partCategoryService.GetPartCategoryById(id, ct);
        var response = mapper.Map<PartCategoryResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreatePartCategory(
        [FromBody] PartCategoryRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<PartCategoryCreateModel>(request);
        var id = await partCategoryService.CreatePartCategory(createModel, ct);
        
        var createdDto = await partCategoryService.GetPartCategoryById(id, ct);
        var response = mapper.Map<PartCategoryResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetPartCategoryById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdatePartCategory(
        int id, [FromBody]PartCategoryUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<PartCategoryUpdateModel>(request);
        await partCategoryService.UpdatePartCategory(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePartCategory(int id, CancellationToken ct)
    {
        await partCategoryService.DeletePartCategory(id, ct);

        return NoContent();
    }
}


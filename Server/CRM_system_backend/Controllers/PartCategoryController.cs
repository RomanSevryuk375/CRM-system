using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.PartCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.PartCategory;

namespace CRM_system_backend.Controllers;

[Route("api/v1/part-categories")]
[ApiController]
public class PartCategoryController(
    IPartCategoryService partCategoryService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<PartCategoryItem>>> GetPartCategories(CancellationToken ct)
    {
        var dto = await partCategoryService.GetPartCategories(ct);

        var response = mapper.Map<PartCategoryResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreatePartCategory(
        [FromBody] PartCategoryRequest request, CancellationToken ct)
    {
        var (partCategory, errors) = PartCategory.Create(
            0,
            request.Name,
            request.Description);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        await partCategoryService.CreatePartCategory(partCategory!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdatePartCategory(
        int id, [FromBody]PartCategoryUpdateRequest request, CancellationToken ct)
    {
        var model = new PartCategoryUpdateModel(
            request.Name,
            request.Description);

        await partCategoryService.UpdatePartCategory(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeletePartCategory(int id, CancellationToken ct)
    {
        await partCategoryService.DeletePartCategory(id, ct);

        return NoContent();
    }
}


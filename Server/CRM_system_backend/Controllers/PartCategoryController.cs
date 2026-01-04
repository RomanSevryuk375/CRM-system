using AutoMapper;
using CRM_system_backend.Contracts.PartCategory;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.PartCategory;
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
    public async Task<ActionResult<List<PartCategoryItem>>> GetPartCategories()
    {
        var dto = await _partCategoryService.GetPartCategories();

        var response = _mapper.Map<PartCategoryResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreatePartCategory([FromBody] PartCategoryRequest request)
    {
        var (partCategory, errors) = PartCategory.Create(
            0,
            request.Name,
            request.Description);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _partCategoryService.CreatePartCategory(partCategory!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdatePartCategory(int id, [FromBody]PartCategoryUpdateRequest request)
    {
        var model = new PartCategoryUpdateModel(
            request.Name,
            request.Description);

        var Id = await _partCategoryService.UpdatePartCategory(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeletePartCategory(int id)
    {
        var Id = await _partCategoryService.DeletePartCategory(id);

        return Ok(Id);
    }
}


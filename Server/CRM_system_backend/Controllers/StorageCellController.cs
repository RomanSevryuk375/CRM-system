using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.StorageCell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.StorageCell;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StorageCellController : ControllerBase
{
    private readonly IStorageCellService _storageCellService;
    private readonly IMapper _mapper;

    public StorageCellController(
        IStorageCellService storageCellService,
        IMapper mapper)
    {
        _storageCellService = storageCellService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<StorageCellItem>>> GetStorageCells(CancellationToken ct)
    {
        var dto = await _storageCellService.GetStorageCells(ct);

        var response = _mapper.Map<List<StorageCellResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> CreateStorageCell([FromBody] StorageCellRequest request, CancellationToken ct)
    {
        var (cell, errors) = StorageCell.Create(
            0,
            request.Rack,
            request.Shelf);
        
        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _storageCellService.CreateStorageCell(cell!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateStorageCell(int id, [FromBody] StorageCellUpdateRequest request, CancellationToken ct)
    {
        var model = new StorageCellUpdateModel(
            request.Rack,
            request.Shelf);

        var Id = await _storageCellService.UpdateStorageCell(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteStorageCell(int id, CancellationToken ct)
    {
        var Id = await _storageCellService.DeleteStorageCell(id, ct);

        return Ok(Id);
    }
}

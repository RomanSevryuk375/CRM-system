using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.StorageCell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.StorageCell;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/storage-cells")]
public class StorageCellController(
    IStorageCellService storageCellService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<StorageCellResponse>>> GetStorageCells(CancellationToken ct)
    {
        var dto = await storageCellService.GetStorageCells(ct);
        var response = mapper.Map<List<StorageCellResponse>>(dto);

        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<StorageCellResponse>> GetStorageCellById(int id, CancellationToken ct)
    {
        var dto = await storageCellService.GetStorageCellById(id, ct);
        var response = mapper.Map<StorageCellResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateStorageCell(
        [FromBody] StorageCellRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<StorageCellCreateModel>(request);
        var id = await storageCellService.CreateStorageCell(createModel, ct);
        
        var createdDto = await storageCellService.GetStorageCellById(id, ct);
        var response = mapper.Map<StorageCellResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetStorageCellById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateStorageCell(
        int id, [FromBody] StorageCellUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<StorageCellUpdateModel>(request);
        await storageCellService.UpdateStorageCell(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteStorageCell(
        int id, CancellationToken ct)
    {
        await storageCellService.DeleteStorageCell(id, ct);

        return NoContent();
    }
}

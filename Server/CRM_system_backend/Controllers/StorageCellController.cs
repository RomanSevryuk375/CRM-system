using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.StorageCell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.StorageCell;

namespace CRM_system_backend.Controllers;

[Route("api/v1/storage-cells")]
[ApiController]
public class StorageCellController(
    IStorageCellService storageCellService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<StorageCellItem>>> GetStorageCells(CancellationToken ct)
    {
        var dto = await storageCellService.GetStorageCells(ct);

        var response = mapper.Map<List<StorageCellResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateStorageCell(
        [FromBody] StorageCellRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<StorageCellCreateModel>(request);

        await storageCellService.CreateStorageCell(createModel, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateStorageCell(
        int id, [FromBody] StorageCellUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<StorageCellUpdateModel>(request);

        await storageCellService.UpdateStorageCell(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteStorageCell(
        int id, CancellationToken ct)
    {
        await storageCellService.DeleteStorageCell(id, ct);

        return NoContent();
    }
}

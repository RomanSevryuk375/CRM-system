using AutoMapper;
using CRM_system_backend.Contracts.StorageCell;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.StorageCell;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<StorageCellItem>>> GetStorageCells()
    {
        var dto = await _storageCellService.GetStorageCells();

        var response = _mapper.Map<List<StorageCellResponse>>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateStorageCell([FromBody] StorageCellRequest request)
    {
        var (cell, errors) = StorageCell.Create(
            0,
            request.Rack,
            request.Shelf);
        
        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _storageCellService.CreateStorageCell(cell!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateStorageCell(int id, [FromBody] StorageCellUpdateRequest request)
    {
        var model = new StorageCellUpdateModel(
            request.Rack,
            request.Shelf);

        var Id = await _storageCellService.UpdateStorageCell(id, model);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteStorageCell(int id)
    {
        var Id = await _storageCellService.DeleteStorageCell(id);

        return Ok(Id);
    }
}

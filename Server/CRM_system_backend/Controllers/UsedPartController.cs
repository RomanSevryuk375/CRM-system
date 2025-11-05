using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]

public class UsedPartController : ControllerBase
{
    private readonly IUsedPartService _usedPartService;

    public UsedPartController(IUsedPartService usedPartService)
    {
        _usedPartService = usedPartService;
    }

    [HttpGet]

    public async Task<ActionResult<List<UsedPart>>> GetUsedPart()
    {
        var usedParts = await _usedPartService.GetUsedPart();

        var response = usedParts
            .Select(u => new UsedPart(
                u.Id,
                u.OrderId,
                u.SupplierId,
                u.Name,
                u.Article,
                u.Quantity,
                u.UnitPrice,
                u.Sum))
            .ToList();

        return Ok(response);
    }

    [HttpGet("with-info")]
    
    public async Task<ActionResult<List<UsedPartWithInfoDto>>> GetUsedPartWithInfo()
    {
        var dtos = await _usedPartService.GetUsedPartWithInfo();

        var response = dtos
            .Select(u => new UsedPartWithInfoDto(
                u.Id,
                u.OrderId,
                u.SupplierName,
                u.Name,
                u.Article,
                u.Quantity,
                u.UnitPrice,
                u.Sum))
            .ToList();

        return Ok(response);
    }

    [HttpPost]

    public async Task<ActionResult<int>> CreateUsedPart([FromBody] UsedPartRequest request)
    {
        var (usedPart, error) = UsedPart.Create(
            0,
            request.OrderId,
            request.SupplierId,
            request.Name,
            request.Article,
            request.Quantity,
            request.UnitPrice,
            request.Sum);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var usedPartId = await _usedPartService.CreateUsedPart(usedPart);

        return Ok(usedPartId);
    }

    [HttpPut]

    public async Task<ActionResult<int>> UpdateUsedPart([FromBody] UsedPartRequest request, int id)
    {
        var result = await _usedPartService.UpdateUsedPart(
            id,
            request.OrderId,
            request.SupplierId,
            request.Name,
            request.Article,
            request.Quantity,
            request.UnitPrice,
            request.Sum);

        return Ok(result);
    }

    [HttpDelete]

    public async Task<ActionResult<int>> DeleteUsedPart(int id)
    {
        var result = await _usedPartService.DeleteUsedPart(id);

        return Ok(result);
    }
}

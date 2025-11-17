using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<UsedPart>>> GetUsedPart(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var usedParts = await _usedPartService.GetPagedUsedPart(page, limit);
        var totalCount = await _usedPartService.GetCountUsedPart();

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

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("with-info")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<UsedPartWithInfoDto>>> GetUsedPartWithInfo(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var dtos = await _usedPartService.GetPagedUsedPartWithInfo(page, limit);
        var totalCount = await _usedPartService.GetCountUsedPart();

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

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }
    [HttpGet("InWork")]
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<List<UsedPartWithInfoDto>>> GetWorkerUsedPart(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var usedParts = await _usedPartService.GetPagedWorkerUsedPart(userId, page, limit);
        var totalCount = await _usedPartService.GetCountWorkerUsedPart(userId);

        var response = usedParts
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

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    //[Authorize(Policy = "WorkerPolicy")]
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
            0);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var usedPartId = await _usedPartService.CreateUsedPart(usedPart);

        return Ok(usedPartId);
    }

    [HttpPut("${id}")]
    [Authorize(Policy = "AdminPolicy")]
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
            0);

        return Ok(result);
    }

    [HttpDelete("${id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteUsedPart(int id)
    {
        var result = await _usedPartService.DeleteUsedPart(id);

        return Ok(result);
    }
}

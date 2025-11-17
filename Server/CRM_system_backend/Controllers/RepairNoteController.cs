using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RepairNoteController : ControllerBase
{
    private readonly IRepairNoteService _repairNoteService;

    public RepairNoteController(IRepairNoteService repairNoteService)
    {
        _repairNoteService = repairNoteService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<RepairNote>>> GetRepairNote(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var repairHistory = await _repairNoteService.GetPagedRepairNote(page, limit);
        var totalCount = await _repairNoteService.GetCountRepairNote();

        var response = repairHistory
            .Select(r => new RepairNoteResponse(
                r.Id,
                r.OrderId,
                r.CarId,
                r.WorkDate,
                r.ServiceSum)).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("with-info")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<RepairNoteWithInfoDto>>> GetRepairNoteWithInfo(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var dtos = await _repairNoteService.GetPagedRepairNoteWithInfo(page, limit);
        var totalCount = await _repairNoteService.GetCountRepairNote();

        var response = dtos
            .Select(r => new RepairNoteWithInfoDto(
                r.Id,
                r.OrderId,
                r.CarInfo,
                r.Date,
                r.ServiceSum)).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("My")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<List<RepairNoteWithInfoDto>>> GetUserRepairNote(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var totalCount = await _repairNoteService.GetCountUserRepairNote(userId);

        var dtos = await _repairNoteService.GetPagedUserRepairNote(userId, page, limit);

        var response = dtos
            .Select(r => new RepairNoteWithInfoDto(
                r.Id,
                r.OrderId,
                r.CarInfo,
                r.Date,
                r.ServiceSum)).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("InWork")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<List<RepairNoteWithInfoDto>>> GetWorkerRepairNote(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var totalCount = await _repairNoteService.GetCountWorkerRepairNote(userId);
        var dtos = await _repairNoteService.GetPagedWorkerRepairNote(userId, page, limit);

        var response = dtos
            .Select(r => new RepairNoteWithInfoDto(
                r.Id,
                r.OrderId,
                r.CarInfo,
                r.Date,
                r.ServiceSum)).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }
}

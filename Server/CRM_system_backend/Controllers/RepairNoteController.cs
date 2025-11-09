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

    public async Task<ActionResult<List<RepairNote>>> GetRepairNote()
    {
        var repairHistory = await _repairNoteService.GetRepairNote();

        var response = repairHistory
            .Select(r => new RepairNoteResponse(
                r.Id,
                r.OrderId,
                r.CarId,
                r.WorkDate,
                r.ServiceSum)).ToList();

        return Ok(response);
    }

    [HttpGet("with-info")]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<RepairNoteWithInfoDto>>> GetRepairNoteWithInfo()
    {
        var dtos = await _repairNoteService.GetRepairNoteWithInfo();

        var response = dtos
            .Select(r => new RepairNoteWithInfoDto(
                r.Id,
                r.OrderId,
                r.CarInfo,
                r.Date,
                r.ServiceSum)).ToList();

        return Ok(response);
    }

    //user can get by id from jwt
    //[Authorize(Policy = "UserPolicy")]
    //worker can get it by cdr id from order
    //Authorize(Policy = "WorkerPolicy")]
}

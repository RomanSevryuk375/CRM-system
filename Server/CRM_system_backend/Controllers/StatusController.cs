using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet]
    [Authorize(Policy = "UniPolicy")]

    public async Task<ActionResult<List<Status>>> GetStatuses()
    {
        var statuses = await _statusService.GetStatuses();

        var response = statuses
            .Select(s => new StatusResponse(s.Id, s.Name, s.Description))
            .ToList();

        return Ok(response);
    }
}

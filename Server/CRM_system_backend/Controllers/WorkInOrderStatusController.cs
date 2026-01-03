using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkInOrderStatusController : ControllerBase
{
    private readonly IWorkInOrderStatusService _service;

    public WorkInOrderStatusController(IWorkInOrderStatusService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkInOrderStatusItem>>> GetWiOStatuses()
    {
        var dto = await _service.GetWiOStatuses();

        var response = dto.Select(w => new WorkInOrderStatusResponse(
            w.Id,
            w.Name));

        return Ok(response);
    }
}

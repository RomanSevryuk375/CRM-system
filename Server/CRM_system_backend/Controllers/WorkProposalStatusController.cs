using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkProposalStatusController : ControllerBase
{
    private readonly IWorkProposalStatusService _service;

    public WorkProposalStatusController(IWorkProposalStatusService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkProposalStatusItem>>> GetProposalStatuses()
    {
        var dto = await _service.GetProposalStatuses();

        var response = dto.Select(p => new WorkProposalStatusResponse(
            p.id,
            p.name));

        return Ok(response);
    }
}

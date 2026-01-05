using AutoMapper;
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
    private readonly IMapper _mapper;

    public WorkProposalStatusController(
        IWorkProposalStatusService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkProposalStatusItem>>> GetProposalStatuses()
    {
        var dto = await _service.GetProposalStatuses();

        var response = _mapper.Map<List<WorkProposalStatusResponse>>(dto);

        return Ok(response);
    }
}

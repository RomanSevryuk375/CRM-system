using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

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
    public async Task<ActionResult<List<WorkProposalStatusItem>>> GetProposalStatuses(CancellationToken ct)
    {
        var dto = await _service.GetProposalStatuses(ct);

        var response = _mapper.Map<List<WorkProposalStatusResponse>>(dto);

        return Ok(response);
    }
}

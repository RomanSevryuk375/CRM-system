using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkInOrderStatusController : ControllerBase
{
    private readonly IWorkInOrderStatusService _service;
    private readonly IMapper _mapper;

    public WorkInOrderStatusController(
        IWorkInOrderStatusService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkInOrderStatusItem>>> GetWiOStatuses(CancellationToken ct)
    {
        var dto = await _service.GetWiOStatuses(ct);

        var response = _mapper.Map<List<WorkInOrderStatusResponse>>(dto);

        return Ok(response);
    }
}

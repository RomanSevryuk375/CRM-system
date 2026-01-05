using AutoMapper;
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
    private readonly IMapper _mapper;

    public WorkInOrderStatusController(
        IWorkInOrderStatusService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkInOrderStatusItem>>> GetWiOStatuses()
    {
        var dto = await _service.GetWiOStatuses();

        var response = _mapper.Map<List<WorkInOrderStatusResponse>>(dto);

        return Ok(response);
    }
}

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Work;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Work;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;
    private readonly IMapper _mapper;

    public WorkController(
        IWorkService workService,
        IMapper mapper)
    {
        _workService = workService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<WorkItem>>> GetPagedWork([FromQuery] WorkFilter filter, CancellationToken ct)
    {
        var dto = await _workService.GetPagedWork(filter, ct);
        var count = await _workService.GetCountWork(ct);

        var response = _mapper.Map<List<WorkResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> CreateWork([FromBody] WorkRequest request, CancellationToken ct)
    {
        var (work, errors) = Work.Create(
            0,
            request.Title,
            request.Category,
            request.Description,
            request.StandardTime);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var workId = await _workService.CreateWork(work!, ct);

        return Ok(workId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> UpdateWork(long id, [FromBody] WorkRequest request, CancellationToken ct)
    {
        var model = new WorkUpdateModel(
            request.Title,
            request.Category,
            request.Description,
            request.StandardTime);

        var Id = await _workService.UpdateWork(id, model, ct);

        return Ok(Id);
    }

    [HttpDelete("${id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<long>> DeleteWork(long id, CancellationToken ct)
    {
        var Id = await _workService.DeleteWork(id, ct);

        return Ok(Id);
    }
}

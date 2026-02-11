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
[Route("api/v1/works")]
public class WorkController(
    IWorkService workService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<List<WorkItem>>> GetPagedWork(
        [FromQuery] WorkFilter filter, CancellationToken ct)
    {
        var dto = await workService.GetPagedWork(filter, ct);
        var count = await workService.GetCountWork(ct);

        var response = mapper.Map<List<WorkResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateWork(
        [FromBody] WorkRequest request, CancellationToken ct)
    {
        var (work, errors) = Work.Create(
            0,
            request.Title,
            request.Category,
            request.Description,
            request.StandardTime);

        if (errors is not null && errors.Any())
        {
            return BadRequest(errors);
        }

        await workService.CreateWork(work!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateWork(
        long id, [FromBody] WorkRequest request, CancellationToken ct)
    {
        var model = new WorkUpdateModel(
            request.Title,
            request.Category,
            request.Description,
            request.StandardTime);

        await workService.UpdateWork(id, model, ct);

        return NoContent();
    }

    [HttpDelete("${id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteWork(long id, CancellationToken ct)
    {
        await workService.DeleteWork(id, ct);

        return NoContent();
    }
}

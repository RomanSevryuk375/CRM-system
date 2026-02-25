using AutoMapper;
using CRMSystem.Business.Abstractions;
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
    public async Task<ActionResult<List<WorkResponse>>> GetPagedWork(
        [FromQuery] WorkFilter filter, CancellationToken ct)
    {
        var dto = await workService.GetPagedWork(filter, ct);
        var count = await workService.GetCountWork(ct);

        var response = mapper.Map<List<WorkResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<WorkResponse>> GetWorkById(
        long id, CancellationToken ct)
    {
        var dto = await workService.GetWorkById(id, ct);
        var response = mapper.Map<WorkResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> CreateWork(
        [FromBody] WorkRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<WorkCreateModel>(request);
        var id =  await workService.CreateWork(createModel, ct);

        var createdDto = await workService.GetWorkById(id, ct);
        var response = mapper.Map<WorkResponse>(createdDto);
        
        return CreatedAtAction(
            nameof(GetWorkById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateWork(
        long id, [FromBody] WorkRequest request, CancellationToken ct)
    {
        var model = mapper.Map<WorkUpdateModel>(request);
        await workService.UpdateWork(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> DeleteWork(long id, CancellationToken ct)
    {
        await workService.DeleteWork(id, ct);

        return NoContent();
    }
}

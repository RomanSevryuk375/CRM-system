using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.ProjectionModels.Worker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Worker;
using Shared.Filters;


namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/workers")]
public class WorkerController(
    IWorkerService workerService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkerResponse>>> GetPagedWorkers(
        [FromQuery] WorkerFilter filter, CancellationToken ct)
    {
        var dto = await workerService.GetPagedWorkers(filter, ct);
        var count = await workerService.GetCountWorkers(filter, ct);

        var response = mapper.Map<List<WorkerResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<WorkerResponse>> GetWorkerById(
        int id, CancellationToken ct)
    {
        var dto = await workerService.GetWorkerById(id, ct);
        var response = mapper.Map<WorkerResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateWorker(
        [FromBody] WorkerRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<WorkerCreateModel>(request);
        var id = await workerService.CreateWorker(createModel, ct);
        
        var createdDto = await workerService.GetWorkerById(id, ct);
        var response = mapper.Map<WorkerResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetWorkerById), 
            new { id = createdDto.Id }, 
            response);
    }

    [HttpPost("user")]
    public async Task<ActionResult> CreateWorker(
        [FromBody]  WorkerWithUserRequest request, CancellationToken ct)
    {
        var workerCreateModel = mapper.Map<WorkerCreateModel>(request);
        var userCreateModel = mapper.Map<UserCreateModel>(request); 
        var id = await workerService.CreateWorkerWithUser(workerCreateModel, userCreateModel, ct);
        
        var createdDto = await workerService.GetWorkerById(id, ct);
        var response = mapper.Map<WorkerResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetWorkerById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateWorker(
        int id, [FromBody] WorkerUpdateRequest request, CancellationToken ct)
    {
        var model = mapper.Map<WorkerUpdateModel>(request);
        await workerService.UpdateWorker(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteWorker(
        int id, CancellationToken ct)
    {
        await workerService.DeleteWorker(id, ct);

        return NoContent();
    }
}

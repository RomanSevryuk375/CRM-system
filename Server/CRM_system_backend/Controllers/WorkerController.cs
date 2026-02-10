using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
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
    public async Task<ActionResult<List<WorkerItem>>> GetPagedWorkers(
        [FromQuery] WorkerFilter filter, CancellationToken ct)
    {
        var dto = await workerService.GetPagedWorkers(filter, ct);
        var count = await workerService.GetCountWorkers(filter, ct);

        var response = mapper.Map<List<WorkerResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<WorkerItem>> GetWorkerById(
        int id, CancellationToken ct)
    {
        var response = await workerService.GetWorkerById(id, ct);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateWorker(
        [FromBody] WorkerRequest request, CancellationToken ct)
    {
        var (worker, errorsWorker) = Worker.Create(
            0,
            request.UserId,
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

        if (errorsWorker is not null && errorsWorker.Any())
            return BadRequest(errorsWorker);

        var workerId = await workerService.CreateWorker(worker!, ct);

        return CreatedAtAction(
            nameof(GetWorkerById), 
            new { Id = workerId}, 
            null);
    }

    [HttpPost("user")]
    public async Task<ActionResult<int>> CreateWorker(
        [FromBody]  WorkerWithUserRequest request, CancellationToken ct)
    {
        var (user, errorsUser) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);

        if (errorsUser is not null && errorsUser.Any())
        {
            return BadRequest(errorsUser);
        }

        var (worker, errorsWorker) = Worker.Create(
            0,
            1,
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

        if (errorsWorker is not null && errorsWorker.Any())
        {
            return BadRequest(errorsWorker);
        }

        var workerId = await workerService.CreateWorkerWithUser(worker!, user!, ct);

        return CreatedAtAction(
            nameof(GetWorkerById),
            new { Id = workerId },
            null);

    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult> UpdateWorker(
        int id, [FromBody] WorkerUpdateRequest request, CancellationToken ct)
    {
        var model = new WorkerUpdateModel(
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

        await workerService.UpdateWorker(id, model, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteWorker(
        int id, CancellationToken ct)
    {
        await workerService.DeleteWorker(id, ct);

        return NoContent();
    }
}

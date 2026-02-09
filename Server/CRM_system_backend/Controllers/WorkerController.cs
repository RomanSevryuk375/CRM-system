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
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly IWorkerService _workerService;
    private readonly IMapper _mapper;

    public WorkerController(
        IWorkerService workerService,
        IMapper mapper)
    {
        _workerService = workerService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<WorkerItem>>> GetPagedWorkers([FromQuery] WorkerFilter filter, CancellationToken ct)
    {
        var dto = await _workerService.GetPagedWorkers(filter, ct);
        var count = await _workerService.GetCountWorkers(filter, ct);

        var response = _mapper.Map<List<WorkerResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<WorkerItem>> GetWorkerById(int id, CancellationToken ct)
    {
        var response = await _workerService.GetWorkerById(id, ct);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateWorker([FromBody] WorkerRequest request, CancellationToken ct)
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

        var Id = await _workerService.CreateWorker(worker!, ct);

        return Ok(Id);
    }

    [HttpPost("with-user")]
    public async Task<ActionResult<int>> CreateWorker([FromBody]  WorkerWithUserRequest request, CancellationToken ct)
    {
        var (user, errorsUser) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);

        if (errorsUser is not null && errorsUser.Any())
            return BadRequest(errorsUser);

        var (worker, errorsWorker) = Worker.Create(
            0,
            1,
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

        if (errorsWorker is not null && errorsWorker.Any())
            return BadRequest(errorsWorker);

        var Id = await _workerService.CreateWorkerWithUser(worker!, user!, ct);

        return Ok(Id);

    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminWorkerPolicy")]
    public async Task<ActionResult<int>> UpdateWorker(int id, [FromBody] WorkerUpdateRequest request, CancellationToken ct)
    {
        var model = new WorkerUpdateModel(
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

        var result = await _workerService.UpdateWorker(id, model, ct);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteWorker(int id, CancellationToken ct)
    {
        var result = await _workerService.DeleteWorker(id, ct);

        return Ok(result);
    }
}

using CRM_system_backend.Contracts.Worker;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Worker;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly IWorkerService _workerService;

    public WorkerController(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkerItem>>> GetPagedWorkers([FromQuery] WorkerFilter filter)
    {
        var dto = await _workerService.GetPagedWorkers(filter);
        var count = await _workerService.GetCountWorkers(filter);

        var response = dto.Select(w => new WorkerResponse(
            w.id,
            w.userId,
            w.name,
            w.surname,
            w.hourlyRate,
            w.phoneNumber,
            w.email));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerItem>> GetWorkerById(int id)
    {
        var response = await _workerService.GetWorkerById(id);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateWorker([FromBody] WorkerRequest request)
    {
        var (worker, errorsWorker) = Worker.Create(
            0,
            request.userId,
            request.name,
            request.surname,
            request.hourlyRate,
            request.phoneNumber,
            request.email);

        if (errorsWorker is not null && errorsWorker.Any())
            return BadRequest(errorsWorker);

        var Id = await _workerService.CreateWorker(worker!);

        return Ok(Id);
    }

    [HttpPost("with-user")]
    public async Task<ActionResult<int>> CreateWorker([FromBody]  WorkerWithUserRequest request)
    {
        var (user, errorsUser) = CRMSystem.Core.Models.User.Create(
            0,
            request.roleId,
            request.login,
            request.password);

        if (errorsUser is not null && errorsUser.Any())
            return BadRequest(errorsUser);

        var (worker, errorsWorker) = Worker.Create(
            0,
            1,
            request.name,
            request.surname,
            request.hourlyRate,
            request.phoneNumber,
            request.email);

        if (errorsWorker is not null && errorsWorker.Any())
            return BadRequest(errorsWorker);

        var Id = await _workerService.CreateWorkerWithUser(worker!, user!);

        return Ok(Id);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateWorker(int id, [FromBody] WorkerRequest request)
    {
        var model = new WorkerUpdateModel(
            request.name,
            request.surname,
            request.hourlyRate,
            request.phoneNumber,
            request.email);

        var result = await _workerService.UpdateWorker(id, model);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteWorker(int id)
    {
        var result = await _workerService.DeleteWorker(id);

        return Ok(result);
    }
}

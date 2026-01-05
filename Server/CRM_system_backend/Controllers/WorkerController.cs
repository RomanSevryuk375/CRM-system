using AutoMapper;
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
    private readonly IMapper _mapper;

    public WorkerController(
        IWorkerService workerService,
        IMapper mapper)
    {
        _workerService = workerService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkerItem>>> GetPagedWorkers([FromQuery] WorkerFilter filter)
    {
        var dto = await _workerService.GetPagedWorkers(filter);
        var count = await _workerService.GetCountWorkers(filter);

        var response = _mapper.Map<List<WorkerResponse>>(dto);

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
            request.UserId,
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

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

        var Id = await _workerService.CreateWorkerWithUser(worker!, user!);

        return Ok(Id);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateWorker(int id, [FromBody] WorkerRequest request)
    {
        var model = new WorkerUpdateModel(
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

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

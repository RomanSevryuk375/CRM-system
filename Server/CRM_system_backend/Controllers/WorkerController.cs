using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkerController : ControllerBase
{
    private readonly IWorkerService _workerService;
    private readonly IUserService _userService;

    public WorkerController(IWorkerService workerService, IUserService userService)
    {
        _workerService = workerService;
        _userService = userService;
    }

    [HttpGet("with-info")]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<WorkerWithInfoDto>>> GetWorkerWithInfo()
    {
        var dtos = await _workerService.GetWorkersWithInfo();

        var response = dtos.Select(d => new WorkerWithInfoDto(
            d.Id,
            d.UserId,
            d.SpecializationName,
            d.Name,
            d.Surname,
            d.HourlyRate,
            d.PhoneNumber,
            d.Email
        )).ToList();

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<List<WorkerResponse>>> GetAllWorker()
    {
        var workers = await _workerService.GetAllWorkers();

        var response = workers
            .Select(w => new WorkerResponse(
                w.Id, w.UserId, w.SpecializationId, w.Name, w.Surname, w.HourlyRate, w.PhoneNumber, w.Email))
            .ToList();

        return Ok(response);
    }

    [HttpPost("with-user")]

    public async Task<ActionResult<int>> CreateWorker([FromBody]  WorkerRegistreRequest request)
    {
        var (user, errorUser) = CRMSystem.Core.Models.User.Create(
            0,
            request.RoleId,
            request.Login,
            request.Password);

        if (!string.IsNullOrEmpty(errorUser))
            return BadRequest(errorUser);

        var userId = await _userService.CreateUser(user);

        var (worker, error) = Worker.Create(
            0,
            userId,
            request.SpecializationId,
            request.Name,
            request.Surname,
            request.HourlyRate,
            request.PhoneNumber,
            request.Email);

        if (!string.IsNullOrEmpty(error))
        {
            await _userService.DeleteUser(userId);
            return BadRequest(error);
        }

        var workerId = await _workerService.CreateWorker(worker);

        return Ok(new
        {
            Message = "Registration successful",
            UserId = userId,
            WorkerId = workerId
        });

    }

    [HttpPut]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> UpdateWorker([FromBody] WorkerRequest workerRequest, int id)
    {
        var result = await _workerService
            .UpdateWorker(
            id,
            workerRequest.UserId,
            workerRequest.SpecializationId,
            workerRequest.Name,
            workerRequest.Surname,
            workerRequest.HourlyRate,
            workerRequest.PhoneNumber,
            workerRequest.Email);

        return Ok(result);
    }

    [HttpDelete]
    [Authorize(Policy = "AdminPolicy")]

    public async Task<ActionResult<int>> DeleteWorker(int id)
    {
        var result = await _workerService.DeleteWorker(id);

        return Ok(result);
    }

    //worker get it by id from jwt
}

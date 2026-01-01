using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Worker;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class WorkerService : IWorkerService
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<WorkerService> _logger;

    public WorkerService(
        IWorkerRepository workerRepository,
        IUserRepository userRepository,
        ILogger<WorkerService> logger)
    {
        _workerRepository = workerRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<WorkerItem>> GetPagedWorkers(WorkerFilter filter)
    {
        _logger.LogInformation("Getting worker start");

        var worker = await _workerRepository.GetPaged(filter);

        _logger.LogInformation("Getting worker success");

        return worker;
    }

    public async Task<int> GetCountWorkers(WorkerFilter filter)
    {
        _logger.LogInformation("Getting count woker start");

        var count = await _workerRepository.GetCount(filter);

        _logger.LogInformation("Getting count worker success");

        return count;
    }

    public async Task<WorkerItem> GetWorkerById(int id)
    {
        _logger.LogInformation("Getting worker by id start");

        var worker = await _workerRepository.GetById(id);
        if (worker is null)
        {
            _logger.LogError("Worker{workerId} not found", id);
            throw new NotFoundException($"Worker{id} not found");
        }

        _logger.LogInformation("Getting worker by id success");

        return worker;
    }

    public async Task<int> CreateWorker(Worker worker)
    {
        _logger.LogInformation("Creating worker start");

        if (!await _userRepository.Exists(worker.UserId))
        {
            _logger.LogError("User{UserId} not found", worker.UserId);
            throw new NotFoundException($"User{worker.UserId} not found");
        }

        var Id = await _workerRepository.Create(worker);

        _logger.LogInformation("Creating worker success");

        return Id;
    }

    public async Task<int> CreateWorkerWithUser(Worker worker, User user)
    {
        var userId = 0L;
        try
        {
            _logger.LogInformation("Creating user start");

            userId = await _userRepository.Create(user);
            worker.SetUserId(userId);

            var Id = await _workerRepository.Create(worker);

            _logger.LogInformation("Creating worker success");

            return Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create worker. Rolling back user.");
            if (userId > 0)
                await _userRepository.Delete(userId);
            throw;
        }
    }

    public async Task<int> UpdateWorker(int id, WorkerUpdateModel model)
    {
        _logger.LogInformation("Updating worker start");

        var Id = await _workerRepository.Update(id, model);

        _logger.LogInformation("Updating worker success");

        return Id;
    }

    public async Task<int> DeleteWorker(int id)
    {
        _logger.LogInformation("Deleting worker start");

        var Id = await _workerRepository.Delete(id);

        _logger.LogInformation("Deleting worker success");

        return Id;
    }
}

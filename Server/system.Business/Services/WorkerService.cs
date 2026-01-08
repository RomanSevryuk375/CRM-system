using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class WorkerService : IWorkerService
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<WorkerService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public WorkerService(
        IWorkerRepository workerRepository,
        IUserRepository userRepository,
        ILogger<WorkerService> logger,
        IUnitOfWork unitOfWork)
    {
        _workerRepository = workerRepository;
        _userRepository = userRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<WorkerItem>> GetPagedWorkers(WorkerFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting worker start");

        var worker = await _workerRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting worker success");

        return worker;
    }

    public async Task<int> GetCountWorkers(WorkerFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count worker start");

        var count = await _workerRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count worker success");

        return count;
    }

    public async Task<WorkerItem> GetWorkerById(int id, CancellationToken ct)
    {
        _logger.LogInformation("Getting worker by id start");

        var worker = await _workerRepository.GetById(id, ct);
        if (worker is null)
        {
            _logger.LogError("Worker{workerId} not found", id);
            throw new NotFoundException($"Worker{id} not found");
        }

        _logger.LogInformation("Getting worker by id success");

        return worker;
    }

    public async Task<int> CreateWorker(Worker worker, CancellationToken ct)
    {
        _logger.LogInformation("Creating worker start");

        if (!await _userRepository.Exists(worker.UserId, ct))
        {
            _logger.LogError("User{UserId} not found", worker.UserId);
            throw new NotFoundException($"User{worker.UserId} not found");
        }

        var Id = await _workerRepository.Create(worker, ct);

        _logger.LogInformation("Creating worker success");

        return Id;
    }

    public async Task<int> CreateWorkerWithUser(Worker worker, User user, CancellationToken ct)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        long userId;
        try
        {
            _logger.LogInformation("Creating user start");

            userId = await _userRepository.Create(user, ct);
            worker.SetUserId(userId);

            var Id = await _workerRepository.Create(worker, ct);

            _logger.LogInformation("Creating worker success");

            await _unitOfWork.CommitTransactionAsync(ct);

            return Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await _unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<int> UpdateWorker(int id, WorkerUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating worker start");

        var Id = await _workerRepository.Update(id, model, ct);

        _logger.LogInformation("Updating worker success");

        return Id;
    }

    public async Task<int> DeleteWorker(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting worker start");

        var Id = await _workerRepository.Delete(id, ct);

        _logger.LogInformation("Deleting worker success");

        return Id;
    }
}

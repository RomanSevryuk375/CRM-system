using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Worker;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class WorkerService(
    IWorkerRepository workerRepository,
    IUserRepository userRepository,
    IUserContext userContext,
    ILogger<WorkerService> logger,
    IUnitOfWork unitOfWork) : IWorkerService
{
    public async Task<List<WorkerItem>> GetPagedWorkers(WorkerFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting worker start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { WorkerIds = [(int)userContext.ProfileId] };
        }

        var worker = await workerRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting worker success");

        return worker;
    }

    public async Task<int> GetCountWorkers(WorkerFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count worker start");

        var count = await workerRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count worker success");

        return count;
    }

    public async Task<WorkerItem> GetWorkerById(int id, CancellationToken ct)
    {
        logger.LogInformation("Getting worker by id start");

        var worker = await workerRepository.GetById(id, ct);
        if (worker is null)
        {
            logger.LogError("Worker{workerId} not found", id);
            throw new NotFoundException($"Worker{id} not found");
        }

        logger.LogInformation("Getting worker by id success");

        return worker;
    }

    public async Task<int> CreateWorker(Worker worker, CancellationToken ct)
    {
        logger.LogInformation("Creating worker start");

        if (!await userRepository.Exists(worker.UserId, ct))
        {
            logger.LogError("User{UserId} not found", worker.UserId);
            throw new NotFoundException($"User{worker.UserId} not found");
        }

        var Id = await workerRepository.Create(worker, ct);

        logger.LogInformation("Creating worker success");

        return Id;
    }

    public async Task<int> CreateWorkerWithUser(Worker worker, User user, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Creating user start");

            var userId = await userRepository.Create(user, ct);
            worker.SetUserId(userId);

            var Id = await workerRepository.Create(worker, ct);

            logger.LogInformation("Creating worker success");

            await unitOfWork.CommitTransactionAsync(ct);

            return Id;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<int> UpdateWorker(int id, WorkerUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating worker start");

        var Id = await workerRepository.Update(id, model, ct);

        logger.LogInformation("Updating worker success");

        return Id;
    }

    public async Task<int> DeleteWorker(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting worker start");

        var Id = await workerRepository.Delete(id, ct);

        logger.LogInformation("Deleting worker success");

        return Id;
    }
}

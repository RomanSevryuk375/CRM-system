using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.User;
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

    public async Task<int> CreateWorker(WorkerCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating worker start");

        if (!await userRepository.Exists(createModel.UserId, ct))
        {
            logger.LogError("User{UserId} not found", createModel.UserId);
            throw new NotFoundException($"User{createModel.UserId} not found");
        }
        
        var (worker, errors) = Worker.Create(
            0,
            createModel.UserId,
            createModel.Name,
            createModel.Surname,
            createModel.HourlyRate,
            createModel.PhoneNumber,
            createModel.Email);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await workerRepository.Create(worker!, ct);

        logger.LogInformation("Creating worker success");

        return id;
    }

    public async Task<int> CreateWorkerWithUser(
        WorkerCreateModel workerCreateModel,
        UserCreateModel userCreateModel,
        CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Creating user start");
            
            var (user, errorsUser) = User.Create(
                0,
                userCreateModel.RoleId,
                userCreateModel.Login,
                userCreateModel.PasswordHash);

            if (errorsUser is not null && errorsUser.Any())
            {
                throw new ValidationException(string.Join(", ", errorsUser));
            }

            var userId = await userRepository.Create(user!, ct);
            
            var (worker, errorsWorker) = Worker.Create(
                0,
                userId,
                workerCreateModel.Name,
                workerCreateModel.Surname,
                workerCreateModel.HourlyRate,
                workerCreateModel.PhoneNumber,
                workerCreateModel.Email);

            if (errorsWorker is not null && errorsWorker.Any())
            {
                throw new ValidationException(string.Join(", ", errorsWorker));
            }

            var id = await workerRepository.Create(worker!, ct);

            logger.LogInformation("Creating worker success");

            await unitOfWork.CommitTransactionAsync(ct);

            return id;
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

        var workerId = await workerRepository.Update(id, model, ct);

        logger.LogInformation("Updating worker success");

        return workerId;
    }

    public async Task<int> DeleteWorker(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting worker start");

        var workerId = await workerRepository.Delete(id, ct);

        logger.LogInformation("Deleting worker success");

        return workerId;
    }
}

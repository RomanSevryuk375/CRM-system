using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.ProjectionModels.Shift;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class ScheduleService(
    IScheduleRepository scheduleRepository,
    IWorkerRepository workerRepository,
    IShiftRepository shiftRepository,
    IUserContext userContext,
    ILogger<ScheduleService> logger,
    IUnitOfWork unitOfWork) : IScheduleService
{
    public async Task<ScheduleItem> GetScheduleById(int id, CancellationToken ct)
    {
        return await scheduleRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Schedule {id} not found");
    }
    
    public async Task<List<ScheduleItem>> GetPagedSchedules(ScheduleFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting schedules start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { WorkerIds = [(int)userContext.ProfileId] };
        }

        var schedules = await scheduleRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting schedules success");

        return schedules;
    }

    public async Task<int> GetCountSchedules(ScheduleFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count schedules start");

        var count = await scheduleRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count schedules success");

        return count;
    }

    public async Task<int> CreateSchedule(ScheduleCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating schedule start");

        if (!await workerRepository.Exists(createModel.WorkerId, ct))
        {
            logger.LogError("Worker {workerId} not found", createModel.WorkerId);
            throw new NotFoundException($"Worker {createModel.WorkerId} not found");
        }

        if (!await shiftRepository.Exists(createModel.ShiftId, ct))
        {
            logger.LogError("Shift {shiftId} not found", createModel.ShiftId);
            throw new NotFoundException($"Shift {createModel.ShiftId} not found");
        }
        
        var (schedule, errors) = Schedule.Create(
            0,
            createModel.WorkerId,
            createModel.ShiftId,
            createModel.DateTime);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await scheduleRepository.Create(schedule!, ct);

        logger.LogInformation("Creating schedule success");

        return id;
    }

    public async Task<int> CreateWithShift(
        ScheduleCreateModel scheduleCreateModel,
        ShiftCreateModel shiftCreateModel,
        CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Creating shift start");
            
            var (shift, errorsShift) = Shift.Create(
                0,
                shiftCreateModel.Name,
                shiftCreateModel.StartAt,
                shiftCreateModel.EndAt);

            if (errorsShift is not null && errorsShift.Any())
            {
                throw new ValidationException(string.Join(", ", errorsShift));
            }

            var shiftId = await shiftRepository.Create(shift!, ct);

            if (!await workerRepository.Exists(scheduleCreateModel.WorkerId, ct))
            {
                logger.LogError("Worker {workerId} not found", scheduleCreateModel.WorkerId);
                throw new NotFoundException($"Worker {scheduleCreateModel.WorkerId} not found");
            }

            logger.LogInformation("Creating shift success");

            logger.LogInformation("Creating schedule start");
            
            var(schedule, errorsSchedule) = Schedule.Create(
                0,
                scheduleCreateModel.WorkerId,
                shiftId,
                scheduleCreateModel.DateTime);

            if (errorsSchedule is not null && errorsSchedule.Any())
            {
                throw new ValidationException(string.Join(", ", schedule));
            }
            
            var scheduleId = await scheduleRepository.Create(schedule!, ct);

            logger.LogInformation("Creating schedule success");

            await unitOfWork.CommitTransactionAsync(ct);

            return scheduleId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<int> UpdateSchedule(int id, ScheduleUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating schedule start");

        var scheduleId = await scheduleRepository.Update(id, model, ct);

        logger.LogInformation("Updating schedule success");

        return scheduleId;
    }

    public async Task<int> DeleteSchedule(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting schedule start");

        var scheduleId = await scheduleRepository.Delete(id, ct);

        logger.LogInformation("Deleting schedule success");

        return scheduleId;
    }
}

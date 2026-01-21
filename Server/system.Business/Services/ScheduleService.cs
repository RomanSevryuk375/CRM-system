using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Schedule;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IUserContext _userContext;
    private readonly ILogger<ScheduleService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleService(
        IScheduleRepository scheduleRepository,
        IWorkerRepository workerRepository,
        IShiftRepository shiftRepository,
        IUserContext userContext,
        ILogger<ScheduleService> logger,
        IUnitOfWork unitOfWork)
    {
        _scheduleRepository = scheduleRepository;
        _workerRepository = workerRepository;
        _shiftRepository = shiftRepository;
        _userContext = userContext;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ScheduleItem>> GetPagedSchedules(ScheduleFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting schedules start");

        if (_userContext.RoleId != (int)RoleEnum.Manager)
            filter = filter with { WorkerIds = [(int)_userContext.ProfileId] };

        var schedules = await _scheduleRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting schedules success");

        return schedules;
    }

    public async Task<int> GetCountSchedules(ScheduleFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count schedules start");

        var count = await _scheduleRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count schedules success");

        return count;
    }

    public async Task<int> CreateSchedule(Schedule schedule, CancellationToken ct)
    {
        _logger.LogInformation("Creating schedule start");

        if (!await _workerRepository.Exists(schedule.WorkerId, ct))
        {
            _logger.LogError("Worker {workerId} not found", schedule.WorkerId);
            throw new NotFoundException($"Worker {schedule.WorkerId} not found");
        }

        if (!await _shiftRepository.Exists(schedule.ShiftId, ct))
        {
            _logger.LogError("Shift {shiftId} not found", schedule.ShiftId);
            throw new NotFoundException($"Shift {schedule.ShiftId} not found");
        }

        var Id = await _scheduleRepository.Create(schedule, ct);

        _logger.LogInformation("Creating schedule success");

        return Id;
    }

    public async Task<int> CreateWithShift(Schedule schedule, Shift shift, CancellationToken ct)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        int shiftId;
        try
        {
            _logger.LogInformation("Creating shift start");

            shiftId = await _shiftRepository.Create(shift, ct);

            if (!await _workerRepository.Exists(schedule.WorkerId, ct))
            {
                _logger.LogError("Worker {workerId} not found", schedule.WorkerId);
                throw new NotFoundException($"Worker {schedule.WorkerId} not found");
            }

            _logger.LogInformation("Creating shift success");

            _logger.LogInformation("Creating schedule start");

            schedule.SetShiftId(shiftId);
            var scheduleId = await _scheduleRepository.Create(schedule, ct);

            _logger.LogInformation("Creating schedule success");

            await _unitOfWork.CommitTransactionAsync(ct);

            return scheduleId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await _unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<int> UpdateSchedule(int id, ScheduleUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating schedule start");

        var Id = await _scheduleRepository.Update(id, model, ct);

        _logger.LogInformation("Updating schedule success");

        return Id;
    }

    public async Task<int> DeleteSchedule(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting schedule start");

        var Id = await _scheduleRepository.Delete(id, ct);

        _logger.LogInformation("Deleting schedule success");

        return Id;
    }
}

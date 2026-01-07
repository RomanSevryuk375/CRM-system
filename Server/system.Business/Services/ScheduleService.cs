using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly ILogger<ScheduleService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleService(
        IScheduleRepository scheduleRepository,
        IWorkerRepository workerRepository,
        IShiftRepository shiftRepository,
        ILogger<ScheduleService> logger,
        IUnitOfWork unitOfWork)
    {
        _scheduleRepository = scheduleRepository;
        _workerRepository = workerRepository;
        _shiftRepository = shiftRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ScheduleItem>> GetPagedSchedules(ScheduleFilter filter)
    {
        _logger.LogInformation("Getting schedules start");

        var schedules = await _scheduleRepository.GetPaged(filter);

        _logger.LogInformation("Getting schedules success");

        return schedules;
    }

    public async Task<int> GetCountSchedules(ScheduleFilter filter)
    {
        _logger.LogInformation("Getting count schedules start");

        var count = await _scheduleRepository.GetCount(filter);

        _logger.LogInformation("Getting count schedules success");

        return count;
    }

    public async Task<int> CreateSchedule(Schedule schedule)
    {
        _logger.LogInformation("Creating schedule start");

        if (!await _workerRepository.Exists(schedule.WorkerId))
        {
            _logger.LogError("Worker {workerId} not found", schedule.WorkerId);
            throw new NotFoundException($"Worker {schedule.WorkerId} not found");
        }

        if (!await _shiftRepository.Exists(schedule.ShiftId))
        {
            _logger.LogError("Shift {shiftId} not found", schedule.ShiftId);
            throw new NotFoundException($"Shift {schedule.ShiftId} not found");
        }

        var Id = await _scheduleRepository.Create(schedule);

        _logger.LogInformation("Creating schedule success");

        return Id;
    }

    public async Task<int> CreateWithShift(Schedule schedule, Shift shift)
    {
        await _unitOfWork.BeginTransactionAsync();

        int shiftId;
        try
        {
            _logger.LogInformation("Creating shift start");

            shiftId = await _shiftRepository.Create(shift);

            if (!await _workerRepository.Exists(schedule.WorkerId))
            {
                _logger.LogError("Worker {workerId} not found", schedule.WorkerId);
                throw new NotFoundException($"Worker {schedule.WorkerId} not found");
            }

            _logger.LogInformation("Creating shift success");

            _logger.LogInformation("Creating schedule start");

            schedule.SetShiftId(shiftId);
            var scheduleId = await _scheduleRepository.Create(schedule);

            _logger.LogInformation("Creating schedule success");

            await _unitOfWork.CommitTransactionAsync();

            return scheduleId;
        }
        catch (ConflictException ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task<int> UpdateSchedule(int id, ScheduleUpdateModel model)
    {
        _logger.LogInformation("Updating schedule start");

        var Id = await _scheduleRepository.Update(id, model);

        _logger.LogInformation("Updating schedule success");

        return Id;
    }

    public async Task<int> DeleteSchedule(int id)
    {
        _logger.LogInformation("Deleting schedule start");

        var Id = await _scheduleRepository.Delete(id);

        _logger.LogInformation("Deleting schedule success");

        return Id;
    }
}

using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IScheduleService
{
    Task<int> CreateSchedule(Schedule schedule, CancellationToken ct);
    Task<int> CreateWithShift(Schedule schedule, Shift shift, CancellationToken ct);
    Task<int> DeleteSchedule(int id, CancellationToken ct);
    Task<int> GetCountSchedules(ScheduleFilter filter, CancellationToken ct);
    Task<List<ScheduleItem>> GetPagedSchedules(ScheduleFilter filter, CancellationToken ct);
    Task<int> UpdateSchedule(int id, ScheduleUpdateModel model, CancellationToken ct);
}
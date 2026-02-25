using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.ProjectionModels.Shift;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IScheduleService
{
    Task<int> CreateSchedule(ScheduleCreateModel createModel, CancellationToken ct);
    Task<int> CreateWithShift(
        ScheduleCreateModel scheduleCreateModel, 
        ShiftCreateModel shiftCreateModel, 
        CancellationToken ct);
    Task<int> DeleteSchedule(int id, CancellationToken ct);
    Task<int> GetCountSchedules(ScheduleFilter filter, CancellationToken ct);
    Task<ScheduleItem> GetScheduleById(int id, CancellationToken ct);
    Task<List<ScheduleItem>> GetPagedSchedules(ScheduleFilter filter, CancellationToken ct);
    Task<int> UpdateSchedule(int id, ScheduleUpdateModel model, CancellationToken ct);
}
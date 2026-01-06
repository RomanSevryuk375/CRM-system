using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IScheduleService
{
    Task<int> CreateSchedule(Schedule schedule);
    Task<int> CreateWithShift(Schedule schedule, Shift shift);
    Task<int> DeleteSchedule(int id);
    Task<int> GetCountSchedules(ScheduleFilter filter);
    Task<List<ScheduleItem>> GetPagedSchedules(ScheduleFilter filter);
    Task<int> UpdateSchedule(int id, ScheduleUpdateModel model);
}
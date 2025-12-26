using CRMSystem.Core.DTOs.Schedule;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IScheduleRepository
{
    Task<int> Create(Schedule schedule);
    Task<int> Delete(int id);
    Task<int> GetCount(ScheduleFilter filter);
    Task<List<ScheduleItem>> GetPaged(ScheduleFilter filter);
    Task<int> Update(int id, ScheduleUpdateModel model);
    Task<bool> ExistsByDateAndId(int id, DateTime date);
}
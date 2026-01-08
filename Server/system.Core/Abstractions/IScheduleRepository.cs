using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IScheduleRepository
{
    Task<int> Create(Schedule schedule, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<int> GetCount(ScheduleFilter filter, CancellationToken ct);
    Task<List<ScheduleItem>> GetPaged(ScheduleFilter filter, CancellationToken ct);
    Task<int> Update(int id, ScheduleUpdateModel model, CancellationToken ct);
    Task<bool> ExistsByDateAndId(int id, DateTime date, CancellationToken ct);
}
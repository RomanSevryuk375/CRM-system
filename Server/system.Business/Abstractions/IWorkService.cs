using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IWorkService
{
    Task<long> CreateWork(Work work, CancellationToken ct);
    Task<long> DeleteWork(long id, CancellationToken ct);
    Task<int> GetCountWork(CancellationToken ct);
    Task<List<WorkItem>> GetPagedWork(WorkFilter filter, CancellationToken ct);
    Task<long> UpdateWork(long id, WorkUpdateModel model, CancellationToken ct);
}
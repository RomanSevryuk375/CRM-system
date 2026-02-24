using CRMSystem.Core.ProjectionModels.Work;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IWorkService
{
    Task<long> CreateWork(WorkCreateModel createModel, CancellationToken ct);
    Task<long> DeleteWork(long id, CancellationToken ct);
    Task<int> GetCountWork(CancellationToken ct);
    Task<List<WorkItem>> GetPagedWork(WorkFilter filter, CancellationToken ct);
    Task<WorkItem> GetWorkById(long id, CancellationToken ct);
    Task<long> UpdateWork(long id, WorkUpdateModel model, CancellationToken ct);
}
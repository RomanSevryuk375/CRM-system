using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Core.Abstractions;

public interface IWorkRepository
{
    Task<long> Create(Work work, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(CancellationToken ct);
    Task<List<WorkItem>> GetPaged(WorkFilter filter, CancellationToken ct);
    Task<long> Update(long id, WorkUpdateModel model, CancellationToken ct);
    Task<WorkItem?> GetById(long id, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}
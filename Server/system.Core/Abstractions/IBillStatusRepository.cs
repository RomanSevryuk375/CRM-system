using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.BillStatus;

namespace CRMSystem.Core.Abstractions;

public interface IBillStatusRepository
{
    Task<List<BillStatusItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}
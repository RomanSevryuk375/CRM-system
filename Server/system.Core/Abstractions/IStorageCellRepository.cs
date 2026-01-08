using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IStorageCellRepository
{
    Task<int> Create(StorageCell storageCell, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<StorageCellItem>> Get(CancellationToken ct);
    Task<int> Update(int id, StorageCellUpdateModel model, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
    Task<bool> HasOverlaps(string rack, string shelf, CancellationToken ct);
}
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IStorageCellRepository
{
    Task<int> Create(StorageCell storageCell);
    Task<int> Delete(int id);
    Task<List<StorageCellItem>> Get();
    Task<int> Update(int id, StorageCellUpdateModel model);
    Task<bool> Exists(int id);
    Task<bool> HasOverlaps(string rack, string shelf);
}
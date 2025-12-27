using CRMSystem.Core.DTOs.StorageCell;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IStorageCellRepository
{
    Task<int> Create(StorageCell storageCell);
    Task<int> Delete(int id);
    Task<List<StorageCellItem>> Get();
    Task<int> Update(int id, StorageCellUpdateModel model);
    Task<bool> Exists(int id);
    Task<bool> HasOverlaps(string rack, string shelf);
}
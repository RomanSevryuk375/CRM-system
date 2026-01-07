using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IStorageCellService
{
    Task<int> CreateStorageCell(StorageCell storageCell);
    Task<int> DeleteStorageCell(int id);
    Task<List<StorageCellItem>> GetStorageCells();
    Task<int> UpdateStorageCell(int id, StorageCellUpdateModel model);
}
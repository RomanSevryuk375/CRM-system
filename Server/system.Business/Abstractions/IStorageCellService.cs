using CRMSystem.Core.ProjectionModels.StorageCell;

namespace CRMSystem.Business.Abstractions;

public interface IStorageCellService
{
    Task<int> CreateStorageCell(StorageCellCreateModel createModel, CancellationToken ct);
    Task<int> DeleteStorageCell(int id, CancellationToken ct);
    Task<List<StorageCellItem>> GetStorageCells(CancellationToken ct);
    Task<int> UpdateStorageCell(int id, StorageCellUpdateModel model, CancellationToken ct);
}
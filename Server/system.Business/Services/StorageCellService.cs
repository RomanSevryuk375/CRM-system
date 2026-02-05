using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class StorageCellService(
    IStorageCellRepository storageCellRepository,
    ILogger<StorageCellService> logger) : IStorageCellService
{
    public async Task<List<StorageCellItem>> GetStorageCells(CancellationToken ct)
    {
        logger.LogInformation("Getting storage cells start");

        var cells = await storageCellRepository.Get(ct);

        logger.LogInformation("Getting storage cells success");

        return cells;
    }

    public async Task<int> CreateStorageCell(StorageCell storageCell, CancellationToken ct)
    {
        logger.LogInformation("Creating storage cell start");

        if (await storageCellRepository.HasOverlaps(storageCell.Rack, storageCell.Shelf, ct))
        {
            logger.LogError("Storage cell is exist with shelf{shelfName} and rack{rackName}", storageCell.Shelf, storageCell.Rack);
            throw new ConflictException($"Storage cell is exist with shelf{storageCell.Shelf} and rack{storageCell.Rack}");
        }

        var Id = await storageCellRepository.Create(storageCell, ct);

        logger.LogInformation("Creating storage cell success");

        return Id;
    }

    public async Task<int> UpdateStorageCell(int id, StorageCellUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating storage cell start");

        if ((!string.IsNullOrEmpty(model.Shelf) && !string.IsNullOrEmpty(model.Rack))
            && await storageCellRepository.HasOverlaps(model.Rack, model.Shelf, ct))
        {
            logger.LogError("Storage cell is exist with shelf{shelfName} and rack{rackName}", model.Shelf, model.Rack);
            throw new ConflictException($"Storage cell is exist with shelf{model.Shelf} and rack{model.Rack}");
        }

        var Id = await storageCellRepository.Update(id, model, ct);

        logger.LogInformation("Updating storage cell success");

        return Id;
    }

    public async Task<int> DeleteStorageCell(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting storage cell start");

        var Id = await storageCellRepository.Delete(id, ct);

        logger.LogInformation("Deleting storage cell success");

        return Id;
    }
}

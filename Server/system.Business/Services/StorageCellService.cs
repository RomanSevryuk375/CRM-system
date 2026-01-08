using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class StorageCellService : IStorageCellService
{
    private readonly IStorageCellRepository _storageCellRepository;
    private readonly ILogger<StorageCellService> _logger;

    public StorageCellService(
        IStorageCellRepository storageCellRepository,
        ILogger<StorageCellService> logger)
    {
        _storageCellRepository = storageCellRepository;
        _logger = logger;
    }

    public async Task<List<StorageCellItem>> GetStorageCells(CancellationToken ct)
    {
        _logger.LogInformation("Getting storage cells start");

        var cells = await _storageCellRepository.Get(ct);

        _logger.LogInformation("Getting storage cells success");

        return cells;
    }

    public async Task<int> CreateStorageCell(StorageCell storageCell, CancellationToken ct)
    {
        _logger.LogInformation("Creating storage cell start");

        if (await _storageCellRepository.HasOverlaps(storageCell.Rack, storageCell.Shelf, ct))
        {
            _logger.LogError("Storage cell is exist with shelf{shelfName} and rack{rackName}", storageCell.Shelf, storageCell.Rack);
            throw new ConflictException($"Storage cell is exist with shelf{storageCell.Shelf} and rack{storageCell.Rack}");
        }

        var Id = await _storageCellRepository.Create(storageCell, ct);

        _logger.LogInformation("Creating storage cell success");

        return Id;
    }

    public async Task<int> UpdateStorageCell(int id, StorageCellUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating storage cell start");

        if ((!string.IsNullOrEmpty(model.Shelf) && !string.IsNullOrEmpty(model.Rack))
            && await _storageCellRepository.HasOverlaps(model.Rack, model.Shelf, ct))
        {
            _logger.LogError("Storage cell is exist with shelf{shelfName} and rack{rackName}", model.Shelf, model.Rack);
            throw new ConflictException($"Storage cell is exist with shelf{model.Shelf} and rack{model.Rack}");
        }

        var Id = await _storageCellRepository.Update(id, model, ct);

        _logger.LogInformation("Updating storage cell success");

        return Id;
    }

    public async Task<int> DeleteStorageCell(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting storage cell start");

        var Id = await _storageCellRepository.Delete(id, ct);

        _logger.LogInformation("Deleting storage cell success");

        return Id;
    }
}

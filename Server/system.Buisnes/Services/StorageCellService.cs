using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.StorageCell;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<StorageCellItem>> GetStorageCells()
    {
        _logger.LogInformation("Getting storage cells start");

        var cells = await _storageCellRepository.Get();

        _logger.LogInformation("Getting storage cells success");

        return cells;
    }

    public async Task<int> CreateStorageCell(StorageCell storageCell)
    {
        _logger.LogInformation("Creating storage cell start");

        if (await _storageCellRepository.HasOverlaps(storageCell.Rack, storageCell.Shelf))
        {
            _logger.LogError("Sotage cell is exist with shelf{shelfName} and rack{rackName}", storageCell.Shelf, storageCell.Rack);
            throw new ConflictException($"Sotage cell is exist with shelf{storageCell.Shelf} and rack{storageCell.Rack}");
        }

        var Id = await _storageCellRepository.Create(storageCell);

        _logger.LogInformation("Creating storage cell success");

        return Id;
    }

    public async Task<int> UpdateStorageCell(int id, StorageCellUpdateModel model)
    {
        _logger.LogInformation("Updating storage cell start");

        //if ((!string.IsNullOrEmpty(model.shelf) && !string.IsNullOrEmpty(model.rack))
        //    && await _storageCellRepository.HasOverlaps(model.rack, model.shelf))
        //{
        //    _logger.LogError("Sotage cell is exist with shelf{shelfName} and rack{rackName}", model.shelf, model.rack);
        //    throw new ConflictException($"Sotage cell is exist with shelf{model.shelf} and rack{model.rack}");
        //}

        var Id = await _storageCellRepository.Update(id, model);

        _logger.LogInformation("Updating storage cell success");

        return Id;
    }

    public async Task<int> DeleteStorageCell(int id)
    {
        _logger.LogInformation("Deleting storage cell start");

        var Id = await _storageCellRepository.Delete(id);

        _logger.LogInformation("Deleting storage cell success");

        return Id;
    }
}

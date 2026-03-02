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
    public async Task<StorageCellItem> GetStorageCellById(int id, CancellationToken ct)
    {
        return await storageCellRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Cell {id} not found");
    }
    
    public async Task<List<StorageCellItem>> GetStorageCells(CancellationToken ct)
    {
        logger.LogInformation("Getting storage cells start");

        var cells = await storageCellRepository.Get(ct);

        logger.LogInformation("Getting storage cells success");

        return cells;
    }

    public async Task<int> CreateStorageCell(StorageCellCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating storage cell start");

        if (await storageCellRepository.HasOverlaps(createModel.Rack, createModel.Shelf, ct))
        {
            logger.LogError("Storage cell is exist with shelf{shelfName} and rack{rackName}", 
                createModel.Shelf, createModel.Rack);
            
            throw new ConflictException(
                $"Storage cell is exist with shelf{createModel.Shelf} and rack{createModel.Rack}");
        }
        
        var (cell, errors) = StorageCell.Create(
            0,
            createModel.Rack,
            createModel.Shelf);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await storageCellRepository.Create(cell!, ct);

        logger.LogInformation("Creating storage cell success");

        return id;
    }

    public async Task<int> UpdateStorageCell(int id, StorageCellUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating storage cell start");

        if ((!string.IsNullOrEmpty(model.Shelf) && !string.IsNullOrEmpty(model.Rack))
            && await storageCellRepository.HasOverlaps(model.Rack, model.Shelf, ct))
        {
            logger.LogError("Storage cell is exist with shelf{shelfName} and rack{rackName}",
                model.Shelf, model.Rack);
            
            throw new ConflictException(
                $"Storage cell is exist with shelf{model.Shelf} and rack{model.Rack}");
        }

        var cellId = await storageCellRepository.Update(id, model, ct);

        logger.LogInformation("Updating storage cell success");

        return cellId;
    }

    public async Task<int> DeleteStorageCell(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting storage cell start");

        var cellId = await storageCellRepository.Delete(id, ct);

        logger.LogInformation("Deleting storage cell success");

        return cellId;
    }
}

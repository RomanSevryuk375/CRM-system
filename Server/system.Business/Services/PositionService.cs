using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class PositionService(
    IPositionRepository positionRepository,
    IPartRepository partRepository,
    IStorageCellRepository cellRepository,
    IPartCategoryRepository partCategoryRepository,
    ILogger<PositionService> logger,
    IUnitOfWork unitOfWork) : IPositionService
{
    public async Task<List<PositionItem>> GetPagedPositions(PositionFilter positionFilter, CancellationToken ct)
    {
        logger.LogInformation("Getting positions start");

        var positions = await positionRepository.GetPaged(positionFilter, ct);

        logger.LogInformation("Getting positions success");

        return positions;
    }

    public async Task<int> GetCountPositions(PositionFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count positions start");

        var count = await positionRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count positions success");

        return count;
    }

    public async Task<long> CreatePositionWithPart(Position position, Part part, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        long newPartId;
        try
        {
            logger.LogInformation("Creating part start");

            if (!await partCategoryRepository.Exists(part.CategoryId, ct))
            {
                logger.LogError("Part category{categoryId} not found", part.CategoryId);
                throw new NotFoundException($"Part category {part.CategoryId} not found");
            }

            newPartId = await partRepository.Create(part,ct);

            logger.LogInformation("Creating part success");

            logger.LogInformation("Creating position start");

            if (!await cellRepository.Exists(position.CellId, ct))
            {
                logger.LogError("Cell {cellId} not found", position.CellId);
                throw new NotFoundException($"Cell {position.CellId} not found");
            }

            position.SetPartId(newPartId);
            var newPositionId = await positionRepository.Create(position, ct);

            logger.LogInformation("Creating position success");

            await unitOfWork.CommitTransactionAsync(ct);

            return newPositionId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<long> UpdatePosition(long id, PositionUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating position start");

        if (model.CellId.HasValue && !await cellRepository.Exists(model.CellId.Value, ct))
        {
            logger.LogError("Cell {cellId} not found", model.CellId);
            throw new NotFoundException($"Cell {model.CellId} not found");
        }

        var Id = await positionRepository.Update(id, model, ct);

        logger.LogInformation("Updating position success");

        return Id;
    }

    public async Task<long> DeletePosition(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting position start");

        var Id = await positionRepository.Delete(id, ct);

        logger.LogInformation("Deleting position success");

        return Id;
    }
}

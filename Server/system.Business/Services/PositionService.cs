using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Part;
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
    public async Task<PositionItem> GetPositionById(int id, CancellationToken ct)
    {
        return await positionRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Position {id} not found");
    }
    
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

    public async Task<long> CreatePositionWithPart(
        PositionCreateModel positionCreateModel,
        PartCreateModel partCreateModel,
        CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            logger.LogInformation("Creating part start");

            if (!await partCategoryRepository.Exists(partCreateModel.CategoryId, ct))
            {
                logger.LogError("Part category{categoryId} not found", partCreateModel.CategoryId);
                throw new NotFoundException($"Part category {partCreateModel.CategoryId} not found");
            }

            var (part, partErrors) = Part.Create(
                0,
                partCreateModel.CategoryId,
                partCreateModel.OemArticle,
                partCreateModel.ManufacturerArticle,
                partCreateModel.InternalArticle,
                partCreateModel.Description,
                partCreateModel.Name,
                partCreateModel.Manufacturer,
                partCreateModel.Applicability);

            if (partErrors is not null && partErrors.Any())
            {
                throw new ValidationException(string.Join(", ", partErrors));
            }
            
            var newPartId = await partRepository.Create(part!,ct);

            logger.LogInformation("Creating part success");

            logger.LogInformation("Creating position start");

            if (!await cellRepository.Exists(positionCreateModel.CellId, ct))
            {
                logger.LogError("Cell {cellId} not found", positionCreateModel.CellId);
                throw new NotFoundException($"Cell {positionCreateModel.CellId} not found");
            }
            
            var (position, positionErrors) = Position.Create(
                0,
                newPartId,
                positionCreateModel.CellId,
                positionCreateModel.PurchasePrice,
                positionCreateModel.SellingPrice,
                positionCreateModel.Quantity);

            if (positionErrors is not null && positionErrors.Any())
            {
                throw new ValidationException(string.Join(", ", positionErrors));
            }
            
            var newPositionId = await positionRepository.Create(position!, ct);

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

        var positionId = await positionRepository.Update(id, model, ct);

        logger.LogInformation("Updating position success");

        return positionId;
    }

    public async Task<long> DeletePosition(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting position start");

        var positionId = await positionRepository.Delete(id, ct);

        logger.LogInformation("Deleting position success");

        return positionId;
    }
}

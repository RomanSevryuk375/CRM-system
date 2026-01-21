using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;
    private readonly IPartRepository _partRepository;
    private readonly IStorageCellRepository _cellRepository;
    private readonly IPartCategoryRepository _partCategoryRepository;
    private readonly ILogger<PositionService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PositionService(
        IPositionRepository positionRepository,
        IPartRepository partRepository,
        IStorageCellRepository cellRepository,
        IPartCategoryRepository partCategoryRepository,
        ILogger<PositionService> logger,
        IUnitOfWork unitOfWork)
    {
        _positionRepository = positionRepository;
        _partRepository = partRepository;
        _cellRepository = cellRepository;
        _partCategoryRepository = partCategoryRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PositionItem>> GetPagedPositions(PositionFilter positionFilter, CancellationToken ct)
    {
        _logger.LogInformation("Getting positions start");

        var positions = await _positionRepository.GetPaged(positionFilter, ct);

        _logger.LogInformation("Getting positions success");

        return positions;
    }

    public async Task<int> GetCountPositions(PositionFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count positions start");

        var count = await _positionRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count positions success");

        return count;
    }

    public async Task<long> CreatePositionWithPart(Position position, Part part, CancellationToken ct)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        long newPartId;
        try
        {
            _logger.LogInformation("Creating part start");

            if (!await _partCategoryRepository.Exists(part.CategoryId, ct))
            {
                _logger.LogError("Part category{categoryId} not found", part.CategoryId);
                throw new NotFoundException($"Part category {part.CategoryId} not found");
            }

            newPartId = await _partRepository.Create(part,ct);

            _logger.LogInformation("Creating part success");

            _logger.LogInformation("Creating position start");

            if (!await _cellRepository.Exists(position.CellId, ct))
            {
                _logger.LogError("Cell {cellId} not found", position.CellId);
                throw new NotFoundException($"Cell {position.CellId} not found");
            }

            position.SetPartId(newPartId);
            var newPositionId = await _positionRepository.Create(position, ct);

            _logger.LogInformation("Creating position success");

            await _unitOfWork.CommitTransactionAsync(ct);

            return newPositionId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed. Rolling back all changes.");
            
            await _unitOfWork.RollbackAsync(ct);

            throw;
        }
    }

    public async Task<long> UpdatePosition(long id, PositionUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating position start");

        if (model.CellId.HasValue && !await _cellRepository.Exists(model.CellId.Value, ct))
        {
            _logger.LogError("Cell {cellId} not found", model.CellId);
            throw new NotFoundException($"Cell {model.CellId} not found");
        }

        var Id = await _positionRepository.Update(id, model, ct);

        _logger.LogInformation("Updating position success");

        return Id;
    }

    public async Task<long> DeletePosition(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting position start");

        var Id = await _positionRepository.Delete(id, ct);

        _logger.LogInformation("Deleting position success");

        return Id;
    }
}

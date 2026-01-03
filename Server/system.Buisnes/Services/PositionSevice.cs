using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Position;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class PositionSevice : IPositionSrevice
{
    private readonly IPositionRepository _positionRepository;
    private readonly IPartRepository _partRepository;
    private readonly IStorageCellRepository _cellRepository;
    private readonly IPartCategoryRepository _partCategoryRepository;
    private readonly ILogger<PositionSevice> _logger;

    public PositionSevice(
        IPositionRepository positionRepository,
        IPartRepository partRepository,
        IStorageCellRepository cellRepository,
        IPartCategoryRepository partCategoryRepository,
        ILogger<PositionSevice> logger)
    {
        _positionRepository = positionRepository;
        _partRepository = partRepository;
        _cellRepository = cellRepository;
        _partCategoryRepository = partCategoryRepository;
        _logger = logger;
    }

    public async Task<List<PositionItem>> GetGagedPositions(PositionFilter positionFilter)
    {
        _logger.LogInformation("Getting positions start");

        var positions = await _positionRepository.GetPaged(positionFilter);

        _logger.LogInformation("Getting positions success");

        return positions;
    }

    public async Task<int> GetCountPositions(PositionFilter filter)
    {
        _logger.LogInformation("Getting count positions start");

        var count = await _positionRepository.GetCount(filter);

        _logger.LogInformation("Getting count positions success");

        return count;
    }

    public async Task<long> CreatePositionWithPart(Position position, Part part)
    {
        var newPartId = 0L;
        try
        {
            _logger.LogInformation("Creating part start");

            if (!await _partCategoryRepository.Exists(part.CategoryId))
            {
                _logger.LogError("Part category{categoryId} not found", part.CategoryId);
                throw new NotFoundException($"Part category {part.CategoryId} not found");
            }

            newPartId = await _partRepository.Create(part);

            _logger.LogInformation("Creating part success");

            _logger.LogInformation("Creating position start");

            if (!await _cellRepository.Exists(position.CellId))
            {
                _logger.LogError("Cell {cellId} not found", position.CellId);
                throw new NotFoundException($"Cell {position.CellId} not found");
            }

            position.SetPartId(newPartId);
            var newPositionId = await _positionRepository.Create(position);

            _logger.LogInformation("Creating position success");

            return newPositionId;
        }
        catch (ConflictException ex)
        {
            _logger.LogError(ex, "Failed to creat part. Rolling back position");
            if (newPartId > 0)
                await _partRepository.Delete(newPartId);
            throw;
        }
    }

    public async Task<long> UpdatePosition(long id, PositionUpdateModel model)
    {
        _logger.LogInformation("Updating position start");

        if (model.CellId.HasValue && !await _cellRepository.Exists(model.CellId.Value))
        {
            _logger.LogError("Cell {cellId} not found", model.CellId);
            throw new NotFoundException($"Cell {model.CellId} not found");
        }

        var Id = await _positionRepository.Update(id, model);

        _logger.LogInformation("Updating position success");

        return Id;
    }

    public async Task<long> DeletePosition(long id)
    {
        _logger.LogInformation("Deleting position start");

        var Id = await _positionRepository.Delete(id);

        _logger.LogInformation("Deleting position success");

        return Id;
    }
}

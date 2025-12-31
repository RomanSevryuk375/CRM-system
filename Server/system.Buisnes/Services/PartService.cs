using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.DTOs.Part;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class PartService : IPartService
{
    private readonly IPartRepository _partRepository;
    private readonly IPartCategoryRepository _partCategoryRepository;
    private readonly ILogger<PartService> _logger;

    public PartService(
        IPartRepository partRepositiry,
        IPartCategoryRepository partCategoryRepository,
        ILogger<PartService> logger)
    {
        _partRepository = partRepositiry;
        _partCategoryRepository = partCategoryRepository;
        _logger = logger;
    }

    public async Task<List<PartItem>> GetPagedParts(PartFilter filter)
    {
        _logger.LogInformation("Getting part start");

        var parts = await _partRepository.GetPaged(filter);

        _logger.LogInformation("Getting part success");

        return parts;
    }

    public async Task<int> GetCountParts(PartFilter filter)
    {
        _logger.LogInformation("Getting count part start");

        var count = await _partRepository.GetCount(filter);

        _logger.LogInformation("Getting part success");

        return count;
    }

    public async Task<long> CreatePart(Part part)
    {
        _logger.LogInformation("Creating part start");

        if (!await _partCategoryRepository.Exists(part.CategoryId))
        {
            _logger.LogError("Part category{categoryId} not found", part.CategoryId);
            throw new NotFoundException($"Part category {part.CategoryId} not found");
        }

        var Id = await _partRepository.Create(part);

        _logger.LogInformation("Creating part success");

        return Id;
    }

    public async Task<long> UpdatePart(long id, PartUpdateModel model)
    {
        _logger.LogInformation("Updating part start");

        var Id = await _partRepository.Update(id, model);

        _logger.LogInformation("Updating part success");

        return Id;
    }

    public async Task<long> DeletePart(long id)
    {
        _logger.LogInformation("Deleting part start");

        var Id = await _partRepository.Delete(id);

        _logger.LogInformation("Deleting part success");

        return Id;
    }
}

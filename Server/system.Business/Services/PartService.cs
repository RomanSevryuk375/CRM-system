using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class PartService : IPartService
{
    private readonly IPartRepository _partRepository;
    private readonly IPartCategoryRepository _partCategoryRepository;
    private readonly ILogger<PartService> _logger;

    public PartService(
        IPartRepository partRepository,
        IPartCategoryRepository partCategoryRepository,
        ILogger<PartService> logger)
    {
        _partRepository = partRepository;
        _partCategoryRepository = partCategoryRepository;
        _logger = logger;
    }

    public async Task<List<PartItem>> GetPagedParts(PartFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting part start");

        var parts = await _partRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting part success");

        return parts;
    }

    public async Task<int> GetCountParts(PartFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count part start");

        var count = await _partRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting part success");

        return count;
    }

    public async Task<long> CreatePart(Part part, CancellationToken ct)
    {
        _logger.LogInformation("Creating part start");

        if (!await _partCategoryRepository.Exists(part.CategoryId, ct))
        {
            _logger.LogError("Part category{categoryId} not found", part.CategoryId);
            throw new NotFoundException($"Part category {part.CategoryId} not found");
        }

        var Id = await _partRepository.Create(part, ct);

        _logger.LogInformation("Creating part success");

        return Id;
    }

    public async Task<long> UpdatePart(long id, PartUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating part start");

        var Id = await _partRepository.Update(id, model, ct);

        _logger.LogInformation("Updating part success");

        return Id;
    }

    public async Task<long> DeletePart(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting part start");

        var Id = await _partRepository.Delete(id, ct);

        _logger.LogInformation("Deleting part success");

        return Id;
    }
}

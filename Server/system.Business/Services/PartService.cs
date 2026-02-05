using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class PartService(
    IPartRepository partRepository,
    IPartCategoryRepository partCategoryRepository,
    ILogger<PartService> logger) : IPartService
{
    public async Task<List<PartItem>> GetPagedParts(PartFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting part start");

        var parts = await partRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting part success");

        return parts;
    }

    public async Task<int> GetCountParts(PartFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count part start");

        var count = await partRepository.GetCount(filter, ct);

        logger.LogInformation("Getting part success");

        return count;
    }

    public async Task<long> CreatePart(Part part, CancellationToken ct)
    {
        logger.LogInformation("Creating part start");

        if (!await partCategoryRepository.Exists(part.CategoryId, ct))
        {
            logger.LogError("Part category{categoryId} not found", part.CategoryId);
            throw new NotFoundException($"Part category {part.CategoryId} not found");
        }

        var Id = await partRepository.Create(part, ct);

        logger.LogInformation("Creating part success");

        return Id;
    }

    public async Task<long> UpdatePart(long id, PartUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating part start");

        var Id = await partRepository.Update(id, model, ct);

        logger.LogInformation("Updating part success");

        return Id;
    }

    public async Task<long> DeletePart(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting part start");

        var Id = await partRepository.Delete(id, ct);

        logger.LogInformation("Deleting part success");

        return Id;
    }
}

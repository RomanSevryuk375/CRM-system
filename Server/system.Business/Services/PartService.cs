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
    public async Task<PartItem> GetPartById(long id, CancellationToken ct)
    {
        return await partRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Part {id} not found");
    }
    
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

    public async Task<long> CreatePart(PartCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating part start");

        if (!await partCategoryRepository.Exists(createModel.CategoryId, ct))
        {
            logger.LogError("Part category{categoryId} not found", createModel.CategoryId);
            throw new NotFoundException($"Part category {createModel.CategoryId} not found");
        }
        
        var (part, errors) = Part.Create(
            0,
            createModel.CategoryId,
            createModel.OemArticle,
            createModel.ManufacturerArticle,
            createModel.InternalArticle,
            createModel.Description,
            createModel.Name,
            createModel.Manufacturer,
            createModel.Applicability);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await partRepository.Create(part!, ct);

        logger.LogInformation("Creating part success");

        return id;
    }

    public async Task<long> UpdatePart(long id, PartUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating part start");

        var partId = await partRepository.Update(id, model, ct);

        logger.LogInformation("Updating part success");

        return partId;
    }

    public async Task<long> DeletePart(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting part start");

        var partId = await partRepository.Delete(id, ct);

        logger.LogInformation("Deleting part success");

        return partId;
    }
}

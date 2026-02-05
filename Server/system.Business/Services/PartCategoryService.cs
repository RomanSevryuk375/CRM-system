// Ignore Spelling: repo

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class PartCategoryService(
    IPartCategoryRepository repo,
    ILogger<PartCategoryService> logger) : IPartCategoryService
{
    public async Task<List<PartCategoryItem>> GetPartCategories(CancellationToken ct)
    {
        logger.LogInformation("Getting part category start");

        var categories = await repo.Get(ct);

        logger.LogInformation("Getting part category success");

        return categories;
    }

    public async Task<int> CreatePartCategory(PartCategory partCategory, CancellationToken ct)
    {
        logger.LogInformation("Creating part category start");

        if (await repo.NameExists(partCategory.Name, ct))
        {
            logger.LogError("Category with this this name{Name} is exist", partCategory.Name);
            throw new ConflictException($"Category with this this name{partCategory.Name} is exist");
        }

        var Id = await repo.Create(partCategory, ct);

        logger.LogInformation("Creating part category success");

        return Id;
    }

    public async Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating part category start");

        if (!string.IsNullOrEmpty(model.Name) && await repo.NameExists(model.Name, ct))
        {
            logger.LogError("Category with this this name{Name} is exist", model.Name);
            throw new ConflictException($"Category with this this name{model.Name} is exist");
        }

        var Id = await repo.Update(id, model, ct);

        logger.LogInformation("Updating part category success");

        return Id;
    }

    public async Task<int> DeletePartCategory(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting part category start");

        var Id = await repo.Delete(id, ct);

        logger.LogInformation("Deleting part category success");

        return Id;
    }
}

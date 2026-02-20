using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.PartCategory;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using CRMSystem.Business.Extensions;

namespace CRMSystem.Business.Cached;

public class CachedPartCategoryService(
    IPartCategoryService decorated,
    IDistributedCache distributed,
    ILogger<CachedPartCategoryService> logger) : IPartCategoryService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedPartCategoryService)}";

    public async Task<int> CreatePartCategory(PartCategoryCreateModel createModel, CancellationToken ct)
    {
        var id = await decorated.CreatePartCategory(createModel, ct);

        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return id;
    }

    public async Task<int> DeletePartCategory(int id, CancellationToken ct)
    {
        var partId = await decorated.DeletePartCategory(id, ct);

        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return partId;
    }

    public async Task<List<PartCategoryItem>> GetPartCategories(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetPartCategories(ct),
            TimeSpan.FromHours(24),
            logger, ct);
    }

    public async Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model, CancellationToken ct)
    {
        var partId = await decorated.UpdatePartCategory(id, model, ct);

        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return partId;
    }
}

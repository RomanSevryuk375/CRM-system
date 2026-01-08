using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using CRMSystem.Business.Extensions;

namespace CRMSystem.Business.Cached;

public class CachedPartCategoryService : IPartCategoryService
{
    private readonly IPartCategoryService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedPartCategoryService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedPartCategoryService)}";

    public CachedPartCategoryService(
        IPartCategoryService decorated,
        IDistributedCache distributed,
        ILogger<CachedPartCategoryService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<int> CreatePartCategory(PartCategory partCategory, CancellationToken ct)
    {
        var Id = await _decorated.CreatePartCategory(partCategory, ct);

        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<int> DeletePartCategory(int id, CancellationToken ct)
    {
        var Id = await _decorated.DeletePartCategory(id, ct);

        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<List<PartCategoryItem>> GetPartCategories(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetPartCategories(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }

    public async Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model, CancellationToken ct)
    {
        var Id = await _decorated.UpdatePartCategory(id, model, ct);

        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return Id;
    }
}

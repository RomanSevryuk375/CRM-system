using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs.PartCategory;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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
    public async Task<int> CreatePartCategory(PartCategory partCategory)
    {
        var Id = await _decorated.CreatePartCategory(partCategory);

        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<int> DeletePartCategory(int id)
    {
        var Id = await _decorated.DeletePartCategory(id);

        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<List<PartCategoryItem>> GetPartCategories()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetPartCategories(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<PartCategoryItem>();
    }

    public async Task<int> UpdatePartCategory(int id, PartCategoryUpdateModel model)
    {
        var Id = await _decorated.UpdatePartCategory(id, model);

        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return Id;
    }
}

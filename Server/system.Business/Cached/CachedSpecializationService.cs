using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.Specialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedSpecializationService(
    ISpecializationService decorated,
    IDistributedCache distributed,
    ILogger<CachedSpecializationService> logger) : ISpecializationService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedSpecializationService)}";

    public async Task<int> CreateSpecialization(SpecializationCreateModel createModel, CancellationToken ct)
    {
        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return await decorated.CreateSpecialization(createModel, ct);
    }

    public async Task<int> DeleteSpecialization(int id, CancellationToken ct)
    {
        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return await decorated.DeleteSpecialization(id, ct);
    }

    public async Task<List<SpecializationItem>> GetSpecializations(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetSpecializations(ct),
            TimeSpan.FromHours(24),
            logger, ct);
    }

    public async Task<SpecializationItem> GetSpecializationById(int id, CancellationToken ct)
    {
        return await decorated.GetSpecializationById(id, ct);
    }

    public async Task<int> UpdateSpecialization(int id, string? name, CancellationToken ct)
    {
        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return await decorated.UpdateSpecialization(id, name, ct);
    }
}

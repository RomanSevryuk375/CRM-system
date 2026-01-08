using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedSpecializationService : ISpecializationService
{
    private readonly ISpecializationService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedSpecializationService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedSpecializationService)}";

    public CachedSpecializationService(
        ISpecializationService decorated,
        IDistributedCache distributed,
        ILogger<CachedSpecializationService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<int> CreateSpecialization(Specialization specialization, CancellationToken ct)
    {
        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.CreateSpecialization(specialization, ct);
    }

    public async Task<int> DeleteSpecialization(int id, CancellationToken ct)
    {
        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteSpecialization(id, ct);
    }

    public async Task<List<SpecializationItem>> GetSpecializations(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetSpecializations(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }

    public async Task<int> UpdateSpecialization(int id, string? name, CancellationToken ct)
    {
        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdateSpecialization(id, name, ct);
    }
}

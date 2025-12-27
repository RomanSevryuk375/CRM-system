using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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
    public async Task<int> CreateSpecialization(Specialization specialization)
    {
        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return await _decorated.CreateSpecialization(specialization);
    }

    public async Task<int> DeleteSpecialization(int id)
    {
        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteSpecialization(id);
    }

    public async Task<List<SpecializationItem>> GetSpecializations()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetSpecializations(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<SpecializationItem>();
    }

    public async Task<int> UpdateSpecialization(int id, string? name)
    {
        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdateSpecialization(id, name);
    }
}

using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Business.Services;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedAbsenceTypeService : IAbsenceTypeService
{
    private readonly IAbsenceTypeService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<AbsenceTypeService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedAbsenceTypeService)}";

    public CachedAbsenceTypeService(
        IAbsenceTypeService decorated,
        IDistributedCache distributed,
        ILogger<AbsenceTypeService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<int> CreateAbsenceType(AbsenceType absenceType, CancellationToken ct)
    {
        var Id = await _decorated.CreateAbsenceType(absenceType, ct);

        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<int> DeleteAbsenceType(int id, CancellationToken ct)
    {
        var Id = await _decorated.DeleteAbsenceType(id, ct);

        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<List<AbsenceTypeItem>> GetAllAbsenceType(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetAllAbsenceType(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }

    public async Task<int> UpdateAbsenceType(int id, string name, CancellationToken ct)
    {
        var Id = await _decorated.UpdateAbsenceType(id, name, ct);

        await _distributed.RemoveAsync(CACHE_KEY, ct);

        _logger.LogInformation("Removing cache success");

        return Id;
    }
}

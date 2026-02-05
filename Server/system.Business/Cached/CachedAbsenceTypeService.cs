using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Business.Services;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedAbsenceTypeService(
    IAbsenceTypeService decorated,
    IDistributedCache distributed,
    ILogger<AbsenceTypeService> logger) : IAbsenceTypeService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedAbsenceTypeService)}";

    public async Task<int> CreateAbsenceType(AbsenceType absenceType, CancellationToken ct)
    {
        var Id = await decorated.CreateAbsenceType(absenceType, ct);

        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<int> DeleteAbsenceType(int id, CancellationToken ct)
    {
        var Id = await decorated.DeleteAbsenceType(id, ct);

        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<List<AbsenceTypeItem>> GetAllAbsenceType(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetAllAbsenceType(ct),
            TimeSpan.FromHours(24),
            logger, ct) ?? [];
    }

    public async Task<int> UpdateAbsenceType(int id, string name, CancellationToken ct)
    {
        var Id = await decorated.UpdateAbsenceType(id, name, ct);

        await distributed.RemoveAsync(CACHE_KEY, ct);

        logger.LogInformation("Removing cache success");

        return Id;
    }
}

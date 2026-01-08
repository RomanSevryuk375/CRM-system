using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedPartSetService : IPartSetService
{
    private readonly IPartSetService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedPartSetService> _logger;

    public CachedPartSetService(
        IPartSetService decorated,
        IDistributedCache distributed,
        ILogger<CachedPartSetService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<long> AddToPartSet(PartSet partSet, CancellationToken ct)
    {
        var key = $"dict_{partSet.OrderId}";

        await _distributed.RemoveAsync(key, ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.AddToPartSet(partSet, ct);
    }

    public async Task<long> DeleteFromPartSet(long id, CancellationToken ct)
    {
        var partSet = await _decorated.GetPartSetById(id, ct);

        var key = $"dict_{partSet.OrderId}";

        await _distributed.RemoveAsync(key, ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteFromPartSet(id, ct);
    }

    public async Task<int> GetCountPartSets(PartSetFilter filter, CancellationToken ct)
    {
        return await _decorated.GetCountPartSets(filter, ct);
    }

    public async Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter, CancellationToken ct)
    {
        return await _decorated.GetPagedPartSets(filter, ct);
    }

    public async Task<PartSetItem> GetPartSetById(long id, CancellationToken ct)
    {
        return await _decorated.GetPartSetById(id, ct);
    }

    public async Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId, CancellationToken ct)
    {
        var key = $"dict_{orderId}";

        return await _distributed.GetOrCreateAsync(
            key,
            () => _decorated.GetPartSetsByOrderId(orderId, ct),
            TimeSpan.FromMinutes(15),
            _logger, ct) ?? [];
    }

    public async Task<long> UpdatePartSet(long id, PartSetUpdateModel model, CancellationToken ct)
    {
        var partSet = await _decorated.GetPartSetById(id, ct);

        var key = $"dict_{partSet.OrderId}";

        await _distributed.RemoveAsync(key, ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdatePartSet(id, model, ct);
    }
}

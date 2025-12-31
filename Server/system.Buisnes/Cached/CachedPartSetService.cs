using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs.PartSet;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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
    public async Task<long> AddToPartSet(PartSet partSet)
    {
        var key = $"dict_{partSet.OrderId}";

        await _distributed.RemoveAsync(key);

        _logger.LogInformation("Removing cache success");

        return await _decorated.AddToPartSet(partSet);
    }

    public async Task<long> DeleteFromPartSet(long id)
    {
        var partSet = await _decorated.GetPartSetById(id);

        var key = $"dict_{partSet.orderId}";

        await _distributed.RemoveAsync(key);

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteFromPartSet(id);
    }

    public async Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter)
    {
        return await _decorated.GetPagedPartSets(filter);
    }

    public async Task<PartSetItem> GetPartSetById(long id)
    {
        return await _decorated.GetPartSetById(id);
    }

    public async Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId)
    {
        var key = $"dict_{orderId}";

        return await _distributed.GetOrCreateAsync(
            key,
            () => _decorated.GetPartSetsByOrderId(orderId),
            TimeSpan.FromMinutes(15),
            _logger) ?? new List<PartSetItem>();
    }

    public async Task<long> UpdatePartSet(long id, PartSetUpdateModel model)
    {
        var partSet = await _decorated.GetPartSetById(id);

        var key = $"dict_{partSet.orderId}";

        await _distributed.RemoveAsync(key);

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdatePartSet(id, model);
    }
}

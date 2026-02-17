using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Cached;

public class CachedPartSetService(
    IPartSetService decorated,
    IDistributedCache distributed,
    ILogger<CachedPartSetService> logger) : IPartSetService
{
    public async Task<long> AddToPartSet(PartSet partSet, CancellationToken ct)
    {
        var key = $"dict_{partSet.OrderId}";

        await distributed.RemoveAsync(key, ct);

        logger.LogInformation("Removing cache success");

        return await decorated.AddToPartSet(partSet, ct);
    }

    public async Task<long> DeleteFromPartSet(long id, CancellationToken ct)
    {
        var partSet = await decorated.GetPartSetById(id, ct);

        var key = $"dict_{partSet.OrderId}";

        await distributed.RemoveAsync(key, ct);

        logger.LogInformation("Removing cache success");

        return await decorated.DeleteFromPartSet(id, ct);
    }

    public async Task<int> GetCountPartSets(PartSetFilter filter, CancellationToken ct)
    {
        return await decorated.GetCountPartSets(filter, ct);
    }

    public async Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter, CancellationToken ct)
    {
        return await decorated.GetPagedPartSets(filter, ct);
    }

    public async Task<PartSetItem> GetPartSetById(long id, CancellationToken ct)
    {
        return await decorated.GetPartSetById(id, ct);
    }

    public async Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId, CancellationToken ct)
    {
        var key = $"dict_{orderId}";

        return await distributed.GetOrCreateAsync(
            key,
            () => decorated.GetPartSetsByOrderId(orderId, ct),
            TimeSpan.FromMinutes(15),
            logger, ct);
    }

    public async Task<long> UpdatePartSet(long id, PartSetUpdateModel model, CancellationToken ct)
    {
        var partSet = await decorated.GetPartSetById(id, ct);

        var key = $"dict_{partSet.OrderId}";

        await distributed.RemoveAsync(key, ct);

        logger.LogInformation("Removing cache success");

        return await decorated.UpdatePartSet(id, model, ct);
    }
}

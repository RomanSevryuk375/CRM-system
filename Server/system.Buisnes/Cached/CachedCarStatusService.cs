using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRMSystem.Buisnes.Cached;

public class CachedCarStatusService : ICarStatusService
{
    private readonly ICarStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedCarStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedCarStatusService)}";

    public CachedCarStatusService(
        ICarStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedCarStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<CarStatusItem>> GetCarStatuses()
    {
        var chachedStatuses = await _distributed.GetStringAsync(CACHE_KEY);

        List<CarStatusItem>? statuses;
        if (chachedStatuses is null)
        {
            statuses = await _decorated.GetCarStatuses();

            if(statuses is null) 
                return new List<CarStatusItem>();

            await _distributed.SetStringAsync(
                CACHE_KEY,
                JsonConvert.SerializeObject(statuses),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                });

            return statuses;
        }

        statuses = JsonConvert.DeserializeObject <List<CarStatusItem>>(chachedStatuses);

        if (statuses is null)
            return new List<CarStatusItem>();

        return statuses;
    }
}

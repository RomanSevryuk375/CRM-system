using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRMSystem.Buisnes.Cached;

public class CachedBillStatusService : IBillStatusService
{
    private readonly IBillStatusService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedBillStatusService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedBillStatusService)}";

    public CachedBillStatusService(
        IBillStatusService decorated,
        IDistributedCache distributed,
        ILogger<CachedBillStatusService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<BillStatusItem>> GetAllBillStatuses()
    {
        var cached = await _distributed.GetStringAsync(CACHE_KEY);

        List<BillStatusItem>? billStatusItems;
        if (string.IsNullOrEmpty(cached))
        {
            _logger.LogInformation("Returning bill status from Db");

            billStatusItems = await _decorated.GetAllBillStatuses();

            if (billStatusItems is null)
                return new List<BillStatusItem>();

            await _distributed.SetStringAsync(
                CACHE_KEY,
                JsonConvert.SerializeObject(billStatusItems),
                new DistributedCacheEntryOptions 
                    { 
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24) 
                    }
                );

            _logger.LogInformation("Caching bill status sucess");

            return billStatusItems;

        }

        _logger.LogInformation("Returning bill status from cache");

        billStatusItems = JsonConvert.DeserializeObject<List<BillStatusItem>>(cached);

        if (billStatusItems is null)
            return new List<BillStatusItem>();

        return billStatusItems;

    }
}

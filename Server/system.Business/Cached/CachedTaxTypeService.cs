using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedTaxTypeService(
    ITaxTypeService decorated,
    IDistributedCache distributed,
    ILogger<CachedTaxTypeService> logger) : ITaxTypeService
{
    private const string CAHCE_KEY = $"Dict_{nameof(CachedTaxTypeService)}";

    public async Task<List<TaxTypeItem>> GetTaxTypes(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CAHCE_KEY,
            () => decorated.GetTaxTypes(ct),
            TimeSpan.FromHours(24),
            logger, ct);
    }
}

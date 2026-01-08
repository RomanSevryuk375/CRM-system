using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedTaxTypeService : ITaxTypeService
{
    private readonly ITaxTypeService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedTaxTypeService> _logger;

    private const string CAHCE_KEY = $"Dict_{nameof(CachedTaxTypeService)}";

    public CachedTaxTypeService(
        ITaxTypeService decorated,
        IDistributedCache distributed,
        ILogger<CachedTaxTypeService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<TaxTypeItem>> GetTaxTypes(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CAHCE_KEY,
            () => _decorated.GetTaxTypes(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }
}

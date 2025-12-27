using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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
    public async Task<List<TaxTypeItem>> GetTaxTypes()
    {
        return await _distributed.GetOrCreateAsync(
            CAHCE_KEY,
            () => _decorated.GetTaxTypes(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<TaxTypeItem>();
    }
}

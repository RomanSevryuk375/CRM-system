using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedPaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedPaymentMethodService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedPaymentMethodService)}";

    public CachedPaymentMethodService(
        IPaymentMethodService decorated,
        IDistributedCache distributed,
        ILogger<CachedPaymentMethodService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<List<PaymentMethodItem>> GetPaymentMethods(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetPaymentMethods(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];

    }
}

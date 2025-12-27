using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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
    public async Task<List<PaymentMethodItem>> GetPaymentMethods()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetPaymentMethods(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<PaymentMethodItem>();

    }
}

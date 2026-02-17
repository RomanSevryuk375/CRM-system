using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedPaymentMethodService(
    IPaymentMethodService decorated,
    IDistributedCache distributed,
    ILogger<CachedPaymentMethodService> logger) : IPaymentMethodService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedPaymentMethodService)}";

    public async Task<List<PaymentMethodItem>> GetPaymentMethods(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetPaymentMethods(ct),
            TimeSpan.FromHours(24),
            logger, ct);

    }
}

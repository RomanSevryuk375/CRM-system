using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedExpenseTypeService(
    IExpenseTypeService decorated,
    IDistributedCache distributed,
    ILogger<CachedExpenseTypeService> logger) : IExpenseTypeService
{
    private const string CACHE_KEY = $"Dict_{nameof(CachedExpenseTypeService)}";

    public async Task<List<ExpenseTypeItem>> GetExpenseType(CancellationToken ct)
    {
        return await distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => decorated.GetExpenseType(ct),
            TimeSpan.FromHours(24),
            logger, ct);
    }
}

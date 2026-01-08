using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Cached;

public class CachedExpenseTypeService : IExpenseTypeService
{
    private readonly IExpenseTypeService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedExpenseTypeService> _logger;

    private const string CACHE_KEY = $"Dict_{nameof(CachedExpenseTypeService)}";

    public CachedExpenseTypeService(
        IExpenseTypeService decorated,
        IDistributedCache distributed,
        ILogger<CachedExpenseTypeService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }

    public async Task<List<ExpenseTypeItem>> GetExpenseType(CancellationToken ct)
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetExpenseType(ct),
            TimeSpan.FromHours(24),
            _logger, ct) ?? [];
    }
}

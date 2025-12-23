using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

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

    public async Task<List<ExpenseTypeItem>> GetExpenseType()
    {
        return await _distributed.GetOrCreateAsync(
            CACHE_KEY,
            () => _decorated.GetExpenseType(),
            TimeSpan.FromHours(24),
            _logger) ?? new List<ExpenseTypeItem>();
    }
}

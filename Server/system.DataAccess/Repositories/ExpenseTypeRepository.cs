using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ExpenseTypeRepository(
    SystemDbContext context,
    IMapper mapper) : IExpenseTypeRepository
{
    public async Task<List<ExpenseTypeItem>> Get(CancellationToken ct)
    {
        return await context.ExpenseTypes
            .AsNoTracking()
            .ProjectTo<ExpenseTypeItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists (int id, CancellationToken ct)
    {
        return await context.ExpenseTypes
            .AsNoTracking()
            .AnyAsync(e => e.Id == id, ct);
    }
}

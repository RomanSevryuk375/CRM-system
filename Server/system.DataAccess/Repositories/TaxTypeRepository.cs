using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class TaxTypeRepository(
    SystemDbContext context,
    IMapper mapper) : ITaxTypeRepository
{
    public async Task<List<TaxTypeItem>> Get(CancellationToken ct)
    {
        return await context.TaxTypes
            .AsNoTracking()
            .ProjectTo<TaxTypeItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.TaxTypes
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, ct);
    }
}

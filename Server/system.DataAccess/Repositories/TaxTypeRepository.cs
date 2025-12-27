using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class TaxTypeRepository : ITaxTypeRepository
{
    private readonly SystemDbContext _context;

    public TaxTypeRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaxTypeItem>> Get()
    {
        var query = _context.TaxTypes.AsNoTracking();

        var projection = query.Select(t => new TaxTypeItem(
            t.Id,
            t.Name));

        return await projection.ToListAsync();
    }
}

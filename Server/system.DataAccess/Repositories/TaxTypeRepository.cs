using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class TaxTypeRepository : ITaxTypeRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public TaxTypeRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TaxTypeItem>> Get(CancellationToken ct)
    {
        return await _context.TaxTypes
            .AsNoTracking()
            .ProjectTo<TaxTypeItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.TaxTypes
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, ct);
    }
}

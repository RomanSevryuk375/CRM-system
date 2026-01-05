using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
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

    public async Task<List<TaxTypeItem>> Get()
    {
        return await _context.TaxTypes
            .AsNoTracking()
            .ProjectTo<TaxTypeItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.TaxTypes
            .AsNoTracking()
            .AnyAsync(t => t.Id == id);
    }
}

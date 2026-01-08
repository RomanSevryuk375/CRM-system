using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ExpenseTypeRepository : IExpenseTypeRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public ExpenseTypeRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ExpenseTypeItem>> Get(CancellationToken ct)
    {
        return await _context.ExpenseTypes
            .AsNoTracking()
            .ProjectTo<ExpenseTypeItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists (int id, CancellationToken ct)
    {
        return await _context.ExpenseTypes
            .AsNoTracking()
            .AnyAsync(e => e.Id == id, ct);
    }
}

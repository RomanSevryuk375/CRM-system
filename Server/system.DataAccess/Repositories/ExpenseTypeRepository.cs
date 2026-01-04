using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
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

    public async Task<List<ExpenseTypeItem>> Get()
    {
        return await _context.ExpenseTypes
            .AsNoTracking()
            .ProjectTo<ExpenseTypeItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists (int id)
    {
        return await _context.ExpenseTypes
            .AsNoTracking()
            .AnyAsync(e => e.Id == id);
    }
}

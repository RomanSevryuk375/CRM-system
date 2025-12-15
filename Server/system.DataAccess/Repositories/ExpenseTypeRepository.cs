using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ExpenseTypeRepository : IExpenseTypeRepository
{
    private readonly SystemDbContext _context;

    public ExpenseTypeRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExpenseTypeItem>> Get()
    {
        var query = _context.ExpenseTypes.AsNoTracking();

        var projection = query.Select(e => new ExpenseTypeItem(
            e.Id,
            e.Name));

        return await projection.ToListAsync();
    }
}

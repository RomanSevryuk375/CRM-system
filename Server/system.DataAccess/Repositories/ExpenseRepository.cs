using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public ExpenseRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<ExpenseEntity> ApplyFilter(IQueryable<ExpenseEntity> query, ExpenseFilter filter)
    {
        if (filter.TaxIds != null && filter.TaxIds.Any())
            query = query.Where(e => filter.TaxIds.Contains(e.TaxId));

        if (filter.PartSetIds != null && filter.PartSetIds.Any())
            query = query.Where(e => filter.PartSetIds.Contains(e.PartSetId));

        if (filter.ExpenseTypeIds != null && filter.ExpenseTypeIds.Any())
            query = query.Where(e => filter.ExpenseTypeIds.Contains(e.ExpenseTypeId));

        return query;
    }

    public async Task<List<ExpenseItem>> GetPaged(ExpenseFilter filter, CancellationToken ct)
    {
        var query = _context.Expenses.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "date" => filter.IsDescending
                ? query.OrderByDescending(e => e.Date)
                : query.OrderBy(e => e.Date),
            "category" => filter.IsDescending
                ? query.OrderByDescending(e => e.Category)
                : query.OrderBy(e => e.Category),
            "tax" => filter.IsDescending
                ? query.OrderByDescending(e => e.Tax == null
                    ? string.Empty
                    : e.Tax.Name)
                : query.OrderBy(e => e.Tax == null
                    ? string.Empty
                    : e.Tax.Name),
            "partset" => filter.IsDescending
                ? query.OrderByDescending(e => e.PartSetId)
                : query.OrderBy(e => e.PartSetId),
            "expensetype" => filter.IsDescending
                ? query.OrderByDescending(e => e.ExpenseType == null
                    ? string.Empty
                    : e.ExpenseType.Name)
                : query.OrderBy(e => e.ExpenseType == null
                    ? string.Empty
                    : e.ExpenseType.Name),
            "sum" => filter.IsDescending
                ? query.OrderByDescending(e => e.Sum)
                : query.OrderBy(e => e.Sum),

            _ => filter.IsDescending
                ? query.OrderByDescending(e => e.Id)
                : query.OrderBy(e => e.Id),
        };

        return await query
            .ProjectTo<ExpenseItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(ExpenseFilter filter, CancellationToken ct)
    {
        var query = _context.Expenses.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<long> Create(Expense expense, CancellationToken ct)
    {
        var expenceEntity = new ExpenseEntity
        {
            Date = expense.Date,
            Category = expense.Category,
            TaxId = expense.TaxId,
            PartSetId = expense.PartSetId,
            ExpenseTypeId = (int)expense.ExpenseTypeId,
            Sum = expense.Sum
        };

        await _context.Expenses.AddAsync(expenceEntity, ct);
        await _context.SaveChangesAsync(ct);

        return expense.Id;
    }

    public async Task<long> Update(long id, ExpenseUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new Exception("Expence not found");

        if (model.Date.HasValue) entity.Date = model.Date.Value;
        if (!string.IsNullOrWhiteSpace(model.Category)) entity.Category = model.Category;
        if (model.ExpenseTypeId.HasValue) entity.ExpenseTypeId = (int)model.ExpenseTypeId.Value;
        if (model.Sum.HasValue) entity.Sum = model.Sum.Value;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var expence = await _context.Expenses
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}

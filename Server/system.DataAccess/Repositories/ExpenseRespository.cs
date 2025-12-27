using CRMSystem.Core.DTOs.Expense;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ExpenseRespository : IExpenseRespository
{
    private readonly SystemDbContext _context;

    public ExpenseRespository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<ExpenseEntity> ApplyFilter(IQueryable<ExpenseEntity> query, ExpenseFilter filter)
    {
        if (filter.taxIds != null && filter.taxIds.Any())
            query = query.Where(e => filter.taxIds.Contains(e.TaxId));

        if (filter.partSetIds != null && filter.partSetIds.Any())
            query = query.Where(e => filter.partSetIds.Contains(e.PartSetId));

        if (filter.expenseTypeIds != null && filter.expenseTypeIds.Any())
            query = query.Where(e => filter.expenseTypeIds.Contains(e.ExpenseTypeId));

        return query;
    }

    public async Task<List<ExpenseItem>> GetPaged(ExpenseFilter filter)
    {
        var query = _context.Expenses.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "date" => filter.isDescending
                ? query.OrderByDescending(e => e.Date)
                : query.OrderBy(e => e.Date),
            "category" => filter.isDescending
                ? query.OrderByDescending(e => e.Category)
                : query.OrderBy(e => e.Category),
            "tax" => filter.isDescending
                ? query.OrderByDescending(e => e.Tax == null
                    ? string.Empty
                    : e.Tax.Name)
                : query.OrderBy(e => e.Tax == null
                    ? string.Empty
                    : e.Tax.Name),
            "partset" => filter.isDescending
                ? query.OrderByDescending(e => e.PartSetId)
                : query.OrderBy(e => e.PartSetId),
            "expensetype" => filter.isDescending
                ? query.OrderByDescending(e => e.ExpenseType == null
                    ? string.Empty
                    : e.ExpenseType.Name)
                : query.OrderBy(e => e.ExpenseType == null
                    ? string.Empty
                    : e.ExpenseType.Name),
            "sum" => filter.isDescending
                ? query.OrderByDescending(e => e.Sum)
                : query.OrderBy(e => e.Sum),

            _ => filter.isDescending
                ? query.OrderByDescending(e => e.Id)
                : query.OrderBy(e => e.Id),
        };

        var projection = query.Select(e => new ExpenseItem(
            e.Id,
            e.Date,
            e.Category,
            e.Tax == null
                ? string.Empty
                : e.Tax.Name,
            e.TaxId,    
            e.PartSetId,
            e.ExpenseType == null
                ? string.Empty
                : e.ExpenseType.Name,
            e.ExpenseTypeId,
            e.Sum));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(ExpenseFilter filter)
    {
        var query = _context.Expenses.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Expense expense)
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

        await _context.Expenses.AddAsync(expenceEntity);
        await _context.SaveChangesAsync();

        return expense.Id;
    }

    public async Task<long> Update(long id, ExpenseUpdateModel model)
    {
        var entity = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Expence not found");

        if (model.date.HasValue) entity.Date = model.date.Value;
        if (!string.IsNullOrWhiteSpace(model.category)) entity.Category = model.category;
        if (model.expenseTypeId.HasValue) entity.ExpenseTypeId = (int)model.expenseTypeId.Value;
        if (model.sum.HasValue) entity.Sum = model.sum.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var expence = await _context.Expenses
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

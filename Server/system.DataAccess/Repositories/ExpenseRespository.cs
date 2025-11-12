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

    public async Task<List<Expense>> Get()
    {
        var expenseEntities = await _context.Expenses
            .AsNoTracking()
            .ToListAsync();

        var expenses = expenseEntities
            .Select(e => Expense.Create(
                e.Id,
                e.Date,
                e.Category,
                e.TaxId,
                e.UsedPartId,
                e.ExpenseType,
                e.Sum).expense)
            .ToList();

        return expenses;
    }

    public async Task<int> Create(Expense expense)
    {
        var (_, error) = Expense.Create(
            0,
            expense.Date,
            expense.Category,
            expense.TaxId,
            expense.UsedPartId,
            expense.ExpenseType,
            expense.Sum);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Expence: {error}");

        var expenceEntity = new ExpenseEntity
        {
            Date = expense.Date,
            Category = expense.Category,
            TaxId = expense.TaxId,
            UsedPartId = expense.UsedPartId,
            ExpenseType = expense.ExpenseType,
            Sum = expense.Sum
        };

        await _context.Expenses.AddAsync(expenceEntity);
        await _context.SaveChangesAsync();

        return expense.Id;
    }

    public async Task<int> Update(int id,DateTime? date, string? category, int? taxId, int? usedPartId, string? expenseType, decimal? sum)
    {
        var expence = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Expence not found");

        if (date.HasValue)
            expence.Date = date.Value;
        if (!string.IsNullOrWhiteSpace(category))
            expence.Category = category;
        if (taxId.HasValue)
            expence.TaxId = taxId.Value;
        if (usedPartId.HasValue)
            expence.UsedPartId = usedPartId.Value;
        if (sum.HasValue)
            expence.Sum = sum.Value;

        await _context.SaveChangesAsync();

        return expence.Id;
    }

    public async Task<int> Delete(int id)
    {
        var expence = await _context.Expenses
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

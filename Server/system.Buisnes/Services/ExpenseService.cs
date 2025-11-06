using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRespository _expenseRespository;
    private readonly IUsedPartRepository _usedPartRepository;
    private readonly ITaxRepository _taxRepository;

    public ExpenseService(
        IExpenseRespository expenseRespository,
        IUsedPartRepository usedPartRepository,
        ITaxRepository taxRepository)
    {
        _expenseRespository = expenseRespository;
        _usedPartRepository = usedPartRepository;
        _taxRepository = taxRepository;
    }

    public async Task<List<Expense>> GetExpenses()
    {
        return await _expenseRespository.Get();
    }

    public async Task<List<ExpensesWitInfoDto>> GetExpensesWithInfo()
    {
        var expenses = await _expenseRespository.Get();
        var taxes = await _taxRepository.Get();
        var usedParts = await _usedPartRepository.Get();

        var response = (from e in expenses
                        join t in taxes on e.TaxId equals t.Id
                        join u in usedParts on e.UsedPartId equals u.Id
                        select new ExpensesWitInfoDto(
                            e.Id,
                            e.Date,
                            e.Category,
                            t.Name,
                            $"{u.Name} (({u.Id}) {u.Article})",
                            e.ExpenseType,
                            e.Sum))
                            .ToList();

        return response;
    }

    public async Task<int> CreateExpense(Expense expense)
    {
        return await _expenseRespository.Create(expense);
    }

    public async Task<int> UpdateExpense(int id, DateTime date, string category, int? taxId, int? usedPartId, string expenseType, decimal sum)
    {
        return await _expenseRespository.Update(id, date, category, taxId, usedPartId, expenseType, sum);
    }

    public async Task<int> DeleteExpense(int id)
    {
        return await _expenseRespository.Delete(id);
    }
}

using CRMSystem.Core.DTOs.Expense;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IExpenseService
{
    Task<long> CreateExpenses(Expense expense);
    Task<long> DeleteExpense(long id);
    Task<int> GetCountExpenses(ExpenseFilter filter);
    Task<List<ExpenseItem>> GetPagedExpenses(ExpenseFilter filter);
    Task<long> UpdateExpense(long id, ExpenseUpdateModel model);
}
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IExpenseService
{
    Task<long> CreateExpenses(Expense expense, CancellationToken ct);
    Task<long> DeleteExpense(long id, CancellationToken ct);
    Task<int> GetCountExpenses(ExpenseFilter filter, CancellationToken ct);
    Task<List<ExpenseItem>> GetPagedExpenses(ExpenseFilter filter, CancellationToken ct);
    Task<long> UpdateExpense(long id, ExpenseUpdateModel model, CancellationToken ct);
}
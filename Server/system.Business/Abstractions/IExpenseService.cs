using CRMSystem.Core.ProjectionModels.Expense;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IExpenseService
{
    Task<long> CreateExpenses(ExpenseCreateModel createModel, CancellationToken ct);
    Task<long> DeleteExpense(long id, CancellationToken ct);
    Task<int> GetCountExpenses(ExpenseFilter filter, CancellationToken ct);
    Task<List<ExpenseItem>> GetPagedExpenses(ExpenseFilter filter, CancellationToken ct);
    Task<ExpenseItem> GetExpenseById(long id, CancellationToken ct);
    Task<long> UpdateExpense(long id, ExpenseUpdateModel model, CancellationToken ct);
}
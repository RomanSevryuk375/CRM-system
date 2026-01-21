using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Core.Abstractions;

public interface IExpenseRepository
{
    Task<long> Create(Expense expense, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(ExpenseFilter filter, CancellationToken ct);
    Task<List<ExpenseItem>> GetPaged(ExpenseFilter filter, CancellationToken ct);
    Task<long> Update(long id, ExpenseUpdateModel model, CancellationToken ct);
}
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IExpenseRepository
{
    Task<long> Create(Expense expense);
    Task<long> Delete(long id);
    Task<int> GetCount(ExpenseFilter filter);
    Task<List<ExpenseItem>> GetPaged(ExpenseFilter filter);
    Task<long> Update(long id, ExpenseUpdateModel model);
}
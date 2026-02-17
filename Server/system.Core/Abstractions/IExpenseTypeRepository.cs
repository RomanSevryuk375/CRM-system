using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.ExpenseType;

namespace CRMSystem.Core.Abstractions;

public interface IExpenseTypeRepository
{
    Task<List<ExpenseTypeItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}
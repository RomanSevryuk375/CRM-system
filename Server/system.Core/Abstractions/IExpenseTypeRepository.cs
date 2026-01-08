using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IExpenseTypeRepository
{
    Task<List<ExpenseTypeItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}
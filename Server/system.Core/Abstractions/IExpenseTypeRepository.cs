using CRMSystem.Core.ProjectionModels.ExpenseType;

namespace CRMSystem.Core.Abstractions;

public interface IExpenseTypeRepository
{
    Task<ExpenseTypeItem?> GetById(int id, CancellationToken ct);
    Task<List<ExpenseTypeItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}
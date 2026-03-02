using CRMSystem.Core.ProjectionModels.ExpenseType;

namespace CRMSystem.Business.Abstractions;

public interface IExpenseTypeService
{
    Task<List<ExpenseTypeItem>> GetExpenseType(CancellationToken ct);
    Task<ExpenseTypeItem> GetExpenseTypeById(int id, CancellationToken ct);
}
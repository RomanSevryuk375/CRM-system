using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IExpenseTypeService
{
    Task<List<ExpenseTypeItem>> GetExpenseType(CancellationToken ct);
}
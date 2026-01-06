using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IExpenseTypeRepository
{
    Task<List<ExpenseTypeItem>> Get();
    Task<bool> Exists(int id);
}
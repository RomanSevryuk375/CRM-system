using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IExpenseTypeService
{
    Task<List<ExpenseTypeItem>> GetExpenseType();
}
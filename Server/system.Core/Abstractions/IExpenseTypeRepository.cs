using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories;

public interface IExpenseTypeRepository
{
    Task<List<ExpenseTypeItem>> Get();
    Task<bool> Exists(int id);
}
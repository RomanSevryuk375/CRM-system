using CRMSystem.Core.DTOs.Expense;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IExpenseRespository
    {
        Task<long> Create(Expense expense);
        Task<long> Delete(long id);
        Task<int> GetCount(ExpenseFilter filter);
        Task<List<ExpenseItem>> GetPaged(ExpenseFilter filter);
        Task<long> Update(long id, ExpenseUpdateModel model);
    }
}
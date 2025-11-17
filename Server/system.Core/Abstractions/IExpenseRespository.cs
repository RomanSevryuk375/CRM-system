using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IExpenseRespository
    {
        Task<List<Expense>> Get();
        Task<int> GetCount();
        Task<int> Create(Expense expense);
        Task<int> Delete(int id);
        Task<int> Update(int id, DateTime? date, string? category, int? taxId, int? usedPartId, string? expenseType, decimal? sum);
    }
}
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IExpenseService
    {
        Task<int> CreateExpense(Expense expense);
        Task<int> DeleteExpense(int id);
        Task<List<Expense>> GetExpenses();
        Task<List<ExpensesWitInfoDto>> GetExpensesWithInfo();
        Task<List<ExpensesWitInfoDto>> GetPagedExpensesWithInfo(int page, int limit);
        Task<int> GetCountExpense();
        Task<int> UpdateExpense(int id, DateTime date, string category, int? taxId, int? usedPartId, string expenseType, decimal sum);
    }
}
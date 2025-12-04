using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface ITaxService
    {
        Task<List<Tax>> GetPagedTaxes(int page, int limit);
        Task<int> GetCountTaxes();
        Task<int> CreateTax(Tax tax);
        Task<int> DeleteTax(int id);
        Task<int> UpdateTax(int id, string? name, decimal? rate, string? type);
    }
}
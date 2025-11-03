using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface ITaxService
    {
        Task<int> CreateTax(Tax tax);
        Task<int> DeleteTax(int id);
        Task<List<Tax>> GetTaxes();
        Task<int> UpdateTax(int id, string name, decimal? rate, string type);
    }
}
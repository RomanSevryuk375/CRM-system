using CRMSystem.Core.DTOs.Tax;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ITaxRepository
    {
        Task<int> Create(Tax tax);
        Task<int> Delete(int id);
        Task<List<TaxItem>> Get(TaxFilter filter);
        Task<int> GetCount(TaxFilter filter);
        Task<int> Update(int id, TaxUpdateModel model);
    }
}
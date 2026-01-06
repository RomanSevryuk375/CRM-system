using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ITaxRepository
{
    Task<int> Create(Tax tax);
    Task<int> Delete(int id);
    Task<List<TaxItem>> Get(TaxFilter filter);
    Task<int> Update(int id, TaxUpdateModel model);
    Task<bool> Exists(int id);
}
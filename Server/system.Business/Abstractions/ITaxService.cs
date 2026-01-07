using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ITaxService
{
    Task<int> CreateTax(Tax tax);
    Task<int> DeleteTax(int id);
    Task<List<TaxItem>> GetTaxes(TaxFilter filter);
    Task<int> UpdateTax(int id, TaxUpdateModel model);
}
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ITaxService
{
    Task<int> CreateTax(Tax tax, CancellationToken ct);
    Task<int> DeleteTax(int id, CancellationToken ct);
    Task<List<TaxItem>> GetTaxes(TaxFilter filter, CancellationToken ct);
    Task<int> UpdateTax(int id, TaxUpdateModel model, CancellationToken ct);
}
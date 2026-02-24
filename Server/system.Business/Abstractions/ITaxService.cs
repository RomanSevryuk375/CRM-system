using CRMSystem.Core.ProjectionModels.Tax;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface ITaxService
{
    Task<int> CreateTax(TaxCreateModel createModel, CancellationToken ct);
    Task<int> DeleteTax(int id, CancellationToken ct);
    Task<List<TaxItem>> GetTaxes(TaxFilter filter, CancellationToken ct);
    Task<TaxItem> GetTaxById(int id, CancellationToken ct);
    Task<int> UpdateTax(int id, TaxUpdateModel model, CancellationToken ct);
}
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ITaxRepository
{
    Task<int> Create(Tax tax, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<TaxItem>> Get(TaxFilter filter, CancellationToken ct);
    Task<int> Update(int id, TaxUpdateModel model, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}
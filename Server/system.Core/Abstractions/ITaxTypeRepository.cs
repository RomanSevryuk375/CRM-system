using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface ITaxTypeRepository
{
    Task<List<TaxTypeItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}
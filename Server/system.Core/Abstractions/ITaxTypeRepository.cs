using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface ITaxTypeRepository
{
    Task<List<TaxTypeItem>> Get();
    Task<bool> Exists(int id);
}
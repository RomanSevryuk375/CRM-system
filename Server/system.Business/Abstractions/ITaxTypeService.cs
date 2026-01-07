using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface ITaxTypeService
{
    Task<List<TaxTypeItem>> GetTaxTypes();
}
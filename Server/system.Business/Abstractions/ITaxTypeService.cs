using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.TaxType;

namespace CRMSystem.Business.Abstractions;

public interface ITaxTypeService
{
    Task<List<TaxTypeItem>> GetTaxTypes(CancellationToken ct);
}
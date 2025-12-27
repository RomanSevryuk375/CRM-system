using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface ITaxTypeService
{
    Task<List<TaxTypeItem>> GetTaxTypes();
}
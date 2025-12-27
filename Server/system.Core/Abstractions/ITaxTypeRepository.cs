using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories;

public interface ITaxTypeRepository
{
    Task<List<TaxTypeItem>> Get();
    Task<bool> Exists(int id);
}
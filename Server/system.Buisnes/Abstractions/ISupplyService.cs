using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISupplyService
{
    Task<long> CreateSupply(Supply supply);
    Task<long> DeleteSupply(long id);
    Task<int> GetCountSupplies(SupplyFilter filter);
    Task<List<SupplyItem>> GetPagedSupplies(SupplyFilter filter);
}
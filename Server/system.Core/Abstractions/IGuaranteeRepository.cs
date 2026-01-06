using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IGuaranteeRepository
{
    Task<long> Create(Guarantee guarantee);
    Task<long> Delete(long id);
    Task<int> GetCount(GuaranteeFilter filter);
    Task<List<GuaranteeItem>> GetPaged(GuaranteeFilter filter);
    Task<long> Update(long id, GuaranteeUpdateModel model);
}
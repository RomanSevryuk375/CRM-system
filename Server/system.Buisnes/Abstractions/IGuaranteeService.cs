using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IGuaranteeService
{
    Task<long> CreateGuarantee(Guarantee guarantee);
    Task<long> DeleteGuarantee(long id);
    Task<int> GetCountGuarantees(GuaranteeFilter filter);
    Task<List<GuaranteeItem>> GetPagedGuarantees(GuaranteeFilter filter);
    Task<long> UpdateGuarantee(long id, GuaranteeUpdateModel model);
}
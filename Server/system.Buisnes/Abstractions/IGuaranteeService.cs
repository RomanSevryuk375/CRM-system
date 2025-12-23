using CRMSystem.Core.DTOs.Guarantee;
using CRMSystem.DataAccess.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IGuaranteeService
{
    Task<long> CreateGuarantee(Guarantee guarantee);
    Task<long> DeleteGuarantee(long id);
    Task<int> GeeCountGuarantees(GuaranteeFilter filter);
    Task<List<GuaranteeItem>> GetPagedGuarantees(GuaranteeFilter filter);
    Task<long> UpdateGuarantee(long id, GuaranteeUpdateModel model);
}
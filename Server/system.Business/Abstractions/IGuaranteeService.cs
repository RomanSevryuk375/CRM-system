using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IGuaranteeService
{
    Task<long> CreateGuarantee(Guarantee guarantee, CancellationToken ct);
    Task<long> DeleteGuarantee(long id, CancellationToken ct);
    Task<int> GetCountGuarantees(GuaranteeFilter filter, CancellationToken ct);
    Task<List<GuaranteeItem>> GetPagedGuarantees(GuaranteeFilter filter, CancellationToken ct);
    Task<long> UpdateGuarantee(long id, GuaranteeUpdateModel model, CancellationToken ct);
}
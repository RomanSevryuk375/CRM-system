using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IGuaranteeRepository
{
    Task<long> Create(Guarantee guarantee, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(GuaranteeFilter filter, CancellationToken ct);
    Task<List<GuaranteeItem>> GetPaged(GuaranteeFilter filter, CancellationToken ct);
    Task<long> Update(long id, GuaranteeUpdateModel model, CancellationToken ct);
}
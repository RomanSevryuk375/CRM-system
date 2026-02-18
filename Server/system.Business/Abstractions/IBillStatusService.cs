using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.BillStatus;

namespace CRMSystem.Business.Abstractions;

public interface IBillStatusService
{
    Task<List<BillStatusItem>> GetAllBillStatuses(CancellationToken ct);
}
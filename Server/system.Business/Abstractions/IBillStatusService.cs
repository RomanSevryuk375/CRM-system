using CRMSystem.Core.ProjectionModels.BillStatus;

namespace CRMSystem.Business.Abstractions;

public interface IBillStatusService
{
    Task<List<BillStatusItem>> GetAllBillStatuses(CancellationToken ct);
    Task<BillStatusItem> GetBillStatusById(int id, CancellationToken ct);
}
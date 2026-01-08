using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IBillStatusService
{
    Task<List<BillStatusItem>> GetAllBillStatuses(CancellationToken ct);
}
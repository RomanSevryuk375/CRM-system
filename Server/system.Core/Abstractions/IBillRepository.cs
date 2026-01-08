using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IBillRepository
{
    Task<long> Create(Bill bill, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(BillFilter filter, CancellationToken ct);
    Task<List<BillItem>> GetPaged(BillFilter filter, CancellationToken ct);
    Task<long> Update(long id, BillUpdateModel model, CancellationToken ct);
    Task<BillItem?> GetByOrderId(long orderId, CancellationToken ct);
    Task<decimal> RecalculateDebt(long Id, CancellationToken ct);
    Task<long> RecalculateAmount(long orderId, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}
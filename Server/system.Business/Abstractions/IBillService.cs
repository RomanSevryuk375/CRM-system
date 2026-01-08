using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IBillService
{
    Task<long> CreateBill(Bill bill, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCountBills(BillFilter filter, CancellationToken ct);
    Task<List<BillItem>> GetPagedBills(BillFilter filter, CancellationToken ct);
    Task<long> UpdateBill(long id, BillUpdateModel model, CancellationToken ct);
    Task<decimal> FetchDebtOfBill(long odrerId, CancellationToken ct);
}
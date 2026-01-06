using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IBillService
{
    Task<long> CreateBill(Bill bill);
    Task<long> Delete(long id);
    Task<int> GetCountBills(BillFilter filter);
    Task<List<BillItem>> GetPagedBills(BillFilter filter);
    Task<long> UpdateBill(long id, BillUpdateModel model);
    Task<decimal> FetchDebtOfBill(long odrerId);
}
using CRMSystem.Core.DTOs.Bill;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IBillService
{
    Task<long> CreateBill(Bill bill);
    Task<long> Delete(long id);
    Task<int> GetCountBills(BillFilter filter);
    Task<List<BillItem>> GetPagedBills(BillFilter filter);
    Task<long> UpdateBill(long id, BillUpdateModel model);
}
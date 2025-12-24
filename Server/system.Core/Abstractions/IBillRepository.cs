using CRMSystem.Core.DTOs.Bill;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IBillRepository
{
    Task<long> Create(Bill bill);
    Task<long> Delete(long id);
    Task<int> GetCount(BillFilter filter);
    Task<List<BillItem>> GetPaged(BillFilter filter);
    Task<long> Update(long id, BillUpdateModel model);
    Task<BillItem?> GetByOrderId(long orderId);
    Task<decimal> RecalculateDebt(long orderId);
    Task<long> RecalculateAmmount(long orderId);
}
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IBillService
    {
        Task<List<Bill>> GetBill();
        Task<List<Bill>> GetBillByUser(int userId);
        Task<int> CreateBill(Bill bill);
    }
}
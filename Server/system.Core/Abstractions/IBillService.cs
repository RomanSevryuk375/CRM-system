using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IBillService
    {
        Task<List<Bill>> GetPagedBill(int page, int limit);
        Task<int> GetBillCount();
        Task<List<Bill>> GetPagedBillByUser(int userId, int page, int limit);
        Task<List<Bill>> GetBillForCar(int carId);
        Task<int> GetBillCountByUser(int userId);
        Task<int> CreateBill(Bill bill);
    }
}
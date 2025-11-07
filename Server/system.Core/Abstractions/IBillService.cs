using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IBillService
    {
        Task<List<Bill>> GatBill();

        Task<int> CreateBill(Bill bill);
    }
}
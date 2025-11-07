using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class BillService : IBillService
{
    private readonly IBillRepository _billRepository;

    public BillService(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<List<Bill>> GatBill()
    {
        return await _billRepository.Get();
    }

    public async Task<int> CreateBill(Bill bill)
    {
        return await _billRepository.Create(bill);
    }
}

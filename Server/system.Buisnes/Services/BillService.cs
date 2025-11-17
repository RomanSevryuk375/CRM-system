using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class BillService : IBillService
{
    private readonly IBillRepository _billRepository;
    private readonly ICarRepository _carRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IClientsRepository _clientsRepository;

    public BillService(
        IBillRepository billRepository,
        ICarRepository carRepository,
        IOrderRepository orderRepository,
        IClientsRepository clientsRepository)
    {
        _billRepository = billRepository;
        _carRepository = carRepository;
        _orderRepository = orderRepository;
        _clientsRepository = clientsRepository;
    }

    public async Task<List<Bill>> GetBill()
    {
        return await _billRepository.Get();
    }

    public async Task<List<Bill>> GetBillByUser(int userId)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        if (client == null)
            return new List<Bill>();
        var clientId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(clientId);
        if (!cars.Any())
            return new List<Bill>();
        var carIds = cars.Select(cr => cr.Id).ToList();

        var orders = await _orderRepository.Get();
        if (!orders.Any())
            return new List<Bill>();
        var orderIds = orders.Where(o => carIds.Contains(o.CarId)).Select(o => o.Id).ToList();

        return await _billRepository.GetByOrderId(orderIds);
    }

    public async Task<int> CreateBill(Bill bill)
    {
        return await _billRepository.Create(bill);
    }
}

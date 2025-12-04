using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class PaymentNoteService : IPaymentNoteService
{
    private readonly IPaymentNoteRepository _paymentNoteRepository;
    private readonly IClientsRepository _clientsRepository;
    private readonly ICarRepository _carRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IBillRepository _billRepository;

    public PaymentNoteService(
        IPaymentNoteRepository paymentNoteRepository,
        IClientsRepository clientsRepository,
        ICarRepository carRepository,
        IOrderRepository orderRepository,
        IBillRepository billRepository)
    {
        _paymentNoteRepository = paymentNoteRepository;
        _clientsRepository = clientsRepository;
        _carRepository = carRepository;
        _orderRepository = orderRepository;
        _billRepository = billRepository;
    }

    public async Task<List<PaymentNote>> GetPagedPaymentNote(int page, int limit)
    {
        return await _paymentNoteRepository.GetPaged(page, limit);
    }

    public async Task<int> GetCountPaymentNote()
    {
        return await _paymentNoteRepository.GetCount();
    }

    public async Task<List<PaymentNote>> GetPagedUserPaymentNote(int userId, int page, int limit)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        var clientId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(clientId);
        var carIds = cars.Select(c => c.Id).ToList();

        var orders = await _orderRepository.GetByCarId(carIds);
        var orderIds = orders.Select(c => c.Id).ToList();

        var bills = await _billRepository.GetByOrderId(orderIds);
        var billIds = bills.Select(c => c.Id).ToList();

        return await _paymentNoteRepository.GetPagedByBillId(billIds, page, limit);
    }

    public async Task<int> GetCountUserPaymentNote(int userId)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        var clientId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(clientId);
        var carIds = cars.Select(c => c.Id).ToList();

        var orders = await _orderRepository.GetByCarId(carIds);
        var orderIds = orders.Select(c => c.Id).ToList();

        var bills = await _billRepository.GetByOrderId(orderIds);
        var billIds = bills.Select(c => c.Id).ToList();

        return await _paymentNoteRepository.GetCountByBillId(billIds);
    }

    public async Task<int> CreatePaymentNote(PaymentNote paymentNote)
    {
        return await _paymentNoteRepository.Create(paymentNote);
    }

    public async Task<int> UpdatePaymentNote(int id, int? billId, DateTime? date, decimal? amount, string? method)
    {
        return await _paymentNoteRepository.Update(id, billId, date, amount, method);
    }

    public async Task<int> DeletePaymentNote(int id)
    {
        return await _paymentNoteRepository.Delete(id);
    }
}

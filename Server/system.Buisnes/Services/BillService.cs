using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Bill;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class BillService : IBillService
{
    private readonly IBillRepository _billRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IBillStatusRepository _statusRepository;
    private readonly ILogger _logger;

    public BillService(
        IBillRepository billRepository,
        IOrderRepository orderRepository,
        IBillStatusRepository statusRepository,
        ILogger<BillService> logger)
    {
        _billRepository = billRepository;
        _orderRepository = orderRepository;
        _statusRepository = statusRepository;
        _logger = logger;
    }

    public async Task<List<BillItem>> GetPagedBills(BillFilter filter)
    {
        _logger.LogInformation("Getting bills start");

        var bill = await _billRepository.GetPaged(filter);

        _logger.LogInformation("Getting bills started");

        return bill;
    }

    public async Task<int> GetCountBills(BillFilter filter)
    {
        _logger.LogInformation("Getting count start");

        var count = await _billRepository.GetCount(filter);

        _logger.LogInformation("Getting count success");

        return count;
    }

    public async Task<long> CreateBill(Bill bill)
    {
        _logger.LogInformation("Creating bill start");

        if (!await _orderRepository.Exists(bill.OrderId))
        {
            _logger.LogError("Order{OrderId} not found", bill.OrderId);
            throw new NotFoundException($"Order{bill.OrderId} not found");
        }

        if (await _orderRepository.GetStatus(bill.OrderId) == (int)OrderStatusEnum.Closed)
        {
            _logger.LogError("Order{OrderId} is closed", bill.OrderId);
            throw new ConflictException($"Order {bill.OrderId} is closed");
        }

        if (!await _statusRepository.Exists((int)bill.StatusId))
        {
            _logger.LogError("Status{StatusId} not found", bill.StatusId);
            throw new NotFoundException($"Status{bill.StatusId} not found");
        }

        _logger.LogInformation("Creating bill success");

        var Id = await _billRepository.Create(bill);

        return Id;
    }

    public async Task<long> UpdateBill(long id, BillUpdateModel model)
    {
        _logger.LogInformation("Updating bill start");

        if (model.statusId is not null)
        {
            if (!await _statusRepository.Exists((int)model.statusId))
            {
                _logger.LogError("Status{StatusId} not found", (int)model.statusId);
                throw new NotFoundException($"Status{(int)model.statusId} not found");
            }
        }

        var Id = await _billRepository.Update(id, model);

        _logger.LogInformation("Updating bill success");

        return Id;
    }

    public async Task<long> Delete(long id)
    {
        _logger.LogInformation("Deleting bill start");

        var Id = await _billRepository.Delete(id);

        _logger.LogInformation("Deleting bill success");

        return id;
    }

    public async Task<decimal> FetchDebtOfBill(long id)
    {
        _logger.LogInformation("Recalculating debt of bill start");

        var debt = await _billRepository.RecalculateDebt(id);

        _logger.LogInformation("Recalculating debt of bill success");

        return debt;
    }
}

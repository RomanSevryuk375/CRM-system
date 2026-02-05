using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class BillService(
    IBillRepository billRepository,
    IOrderRepository orderRepository,
    IBillStatusRepository statusRepository,
    IUserContext userContext,
    ILogger<BillService> logger) : IBillService
{
    private readonly ILogger _logger = logger;

    public async Task<List<BillItem>> GetPagedBills(BillFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting bills start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { ClientIds = [userContext.ProfileId] };
        }

        var bill = await billRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting bills started");

        return bill;
    }

    public async Task<int> GetCountBills(BillFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count start");

        var count = await billRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count success");

        return count;
    }

    public async Task<long> CreateBill(Bill bill, CancellationToken ct)
    {
        _logger.LogInformation("Creating bill start");

        if (!await orderRepository.Exists(bill.OrderId, ct))
        {
            _logger.LogError("Order{OrderId} not found", bill.OrderId);
            throw new NotFoundException($"Order{bill.OrderId} not found");
        }

        if (await orderRepository.GetStatus(bill.OrderId, ct) == (int)OrderStatusEnum.Closed)
        {
            _logger.LogError("Order{OrderId} is closed", bill.OrderId);
            throw new ConflictException($"Order {bill.OrderId} is closed");
        }

        if (!await statusRepository.Exists((int)bill.StatusId, ct))
        {
            _logger.LogError("Status{StatusId} not found", bill.StatusId);
            throw new NotFoundException($"Status{bill.StatusId} not found");
        }

        _logger.LogInformation("Creating bill success");

        var Id = await billRepository.Create(bill, ct);

        return Id;
    }

    public async Task<long> UpdateBill(long id, BillUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating bill start");

        if (model.StatusId is not null)
        {
            if (!await statusRepository.Exists((int)model.StatusId, ct))
            {
                _logger.LogError("Status{StatusId} not found", (int)model.StatusId);
                throw new NotFoundException($"Status{(int)model.StatusId} not found");
            }
        }

        var Id = await billRepository.Update(id, model, ct);

        _logger.LogInformation("Updating bill success");

        return Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting bill start");

        var Id = await billRepository.Delete(id, ct);

        _logger.LogInformation("Deleting bill success");

        return Id;
    }

    public async Task<decimal> FetchDebtOfBill(long id, CancellationToken ct)
    {
        _logger.LogInformation("Recalculating debt of bill start");

        var debt = await billRepository.RecalculateDebt(id, ct);

        _logger.LogInformation("Recalculating debt of bill success");

        return debt;
    }

    public async Task<decimal> RecalculateBillAmount(long id, CancellationToken ct)
    {
        _logger.LogInformation("Recalculating amount of bill start");

        var amount = await billRepository.RecalculateAmount(id, ct);

        _logger.LogInformation("Recalculating amount of bill success");

        return amount;
    }
}

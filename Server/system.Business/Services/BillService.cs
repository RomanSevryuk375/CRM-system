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
    public async Task<BillItem> GetBillById(long id, CancellationToken ct)
    {
        return await billRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Bill {id} not found");
    }

    public async Task<List<BillItem>> GetPagedBills(BillFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting bills start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { ClientIds = [userContext.ProfileId] };
        }

        var bill = await billRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting bills started");

        return bill;
    }

    public async Task<int> GetCountBills(BillFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count start");

        var count = await billRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count success");

        return count;
    }

    public async Task<long> CreateBill(BillCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating bill start");

        if (!await orderRepository.Exists(createModel.OrderId, ct))
        {
            logger.LogError("Order{OrderId} not found", createModel.OrderId);
            throw new NotFoundException($"Order{createModel.OrderId} not found");
        }

        if (await orderRepository.GetStatus(createModel.OrderId, ct) == (int)OrderStatusEnum.Closed)
        {
            logger.LogError("Order{OrderId} is closed", createModel.OrderId);
            throw new ConflictException($"Order {createModel.OrderId} is closed");
        }

        if (!await statusRepository.Exists((int)createModel.StatusId, ct))
        {
            logger.LogError("Status{StatusId} not found", createModel.StatusId);
            throw new NotFoundException($"Status{createModel.StatusId} not found");
        }

        logger.LogInformation("Creating bill success");
        
        var (bill, errors) = Bill.Create(
            0,
            createModel.OrderId,
            createModel.StatusId,
            createModel.CreatedAt,
            createModel.Amount,
            createModel.ActualBillDate);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await billRepository.Create(bill!, ct);

        return id;
    }

    public async Task<long> UpdateBill(long id, BillUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating bill start");
        
        if (model.StatusId is not null && !await statusRepository.Exists((int)model.StatusId, ct))
        {
            logger.LogError("Status{StatusId} not found", (int)model.StatusId);
            throw new NotFoundException($"Status{(int)model.StatusId} not found");
        }

        var billId = await billRepository.Update(id, model, ct);

        logger.LogInformation("Updating bill success");

        return billId;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting bill start");

        var billId = await billRepository.Delete(id, ct);

        logger.LogInformation("Deleting bill success");

        return billId;
    }

    public async Task<decimal> FetchDebtOfBill(long orderId, CancellationToken ct)
    {
        logger.LogInformation("Recalculating debt of bill start");

        var debt = await billRepository.RecalculateDebt(orderId, ct);

        logger.LogInformation("Recalculating debt of bill success");

        return debt;
    }

    public async Task<decimal> RecalculateBillAmount(long id, CancellationToken ct)
    {
        logger.LogInformation("Recalculating amount of bill start");

        var amount = await billRepository.RecalculateAmount(id, ct);

        logger.LogInformation("Recalculating amount of bill success");

        return amount;
    }
}

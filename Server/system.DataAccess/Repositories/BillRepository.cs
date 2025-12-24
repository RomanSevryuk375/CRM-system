using CRMSystem.Core.DTOs.Bill;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class BillRepository : IBillRepository
{
    private readonly SystemDbContext _context;

    public BillRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<BillEntity> ApplyFilter(IQueryable<BillEntity> query, BillFilter filter)
    {
        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(b => filter.OrderIds.Contains(b.OrderId));

        return query;
    }

    public async Task<List<BillItem>> GetPaged(BillFilter filter)
    {
        var query = _context.Bills.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "status" => filter.isDescending
                ? query.OrderByDescending(a => a.Status == null 
                    ? string.Empty
                    : a.Status.Name)
                : query.OrderBy(a => a.Status == null
                    ? string.Empty
                    : a.Status.Name),
            "createat" => filter.isDescending
                ? query.OrderByDescending(a => a.CreatedAt)
                : query.OrderBy(a => a.CreatedAt),
            "amount" => filter.isDescending
                ? query.OrderByDescending(a => a.Amount)
                : query.OrderBy(a => a.Amount),
            "actualbilldate" => filter.isDescending
                ? query.OrderByDescending(a => a.ActualBillDate)
                : query.OrderBy(a => a.ActualBillDate),

            _ => filter.isDescending
                ? query.OrderByDescending(a => a.Id)
                : query.OrderBy(a => a.Id),
        };

        var projection = query.Select(b => new BillItem(
            b.Id,
            b.OrderId,
            b.Status == null
                ? string.Empty
                : $"{b.Status.Name}",
            b.StatusId,
            b.CreatedAt,
            b.Amount,
            b.ActualBillDate));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(BillFilter filter)
    {
        var query = _context.Bills.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<BillItem?> GetByOrderId(long orderId)
    {
        var bill = await _context.Bills
            .AsNoTracking()
            .Where(b => b.OrderId == orderId)
            .Select(b => new BillItem(
            b.Id,
            b.OrderId,
            b.Status == null
                ? string.Empty
                : $"{b.Status.Name}",
            b.StatusId,
            b.CreatedAt,
            b.Amount,
            b.ActualBillDate))
            .FirstOrDefaultAsync();

        return bill;
    }

    public async Task<long> Create(Bill bill)
    {
        var billEntity = new BillEntity
        {
            OrderId = bill.OrderId,
            StatusId = (int)bill.StatusId,
            CreatedAt = bill.CreatedAt,
            Amount = bill.Amount,
            ActualBillDate = bill.ActualBillDate,
        };

        await _context.Bills.AddAsync(billEntity);
        await _context.SaveChangesAsync();

        return billEntity.Id;
    }

    public async Task<long> Update(long id, BillUpdateModel model)
    {
        var entity = await _context.Bills.FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new NotFoundException("Bill not found");

        if (model.statusId.HasValue) entity.StatusId = (int)model.statusId.Value;
        if (model.amount.HasValue) entity.Amount = model.amount.Value;
        if (model.actualBillDate.HasValue) entity.ActualBillDate = model.actualBillDate.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.Bills
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<long> RecalculateAmmount(long orderId)
    {
        var bill = await _context.Bills.FirstOrDefaultAsync(b => b.OrderId == orderId)
            ?? throw new NotFoundException("Bill not found");

        var newSetSum = await _context.PartSets
            .Where(b => b.OrderId == orderId)
            .SumAsync(b => b.SoldPrice * b.Quantity);

        var newWorkInOrderSum = await _context.WorksInOrder
            .Include(w => w.Work)
            .Include(w => w.Worker)
            .Where(b => b.OrderId == orderId)
            .SumAsync(b => (b.Work != null && b.Worker != null)
                ? b.Work.StandardTime * b.Worker.HourlyRate
                : 0m);

        bill.Amount = newSetSum + newWorkInOrderSum;

        await _context.SaveChangesAsync();

        return bill.Id;
    }

    public async Task<decimal> RecalculateDebt(long orderId)
    {
        var bill = await _context.Bills.FirstOrDefaultAsync(b => b.OrderId == orderId)
            ?? throw new NotFoundException("Bill not found");

        var payedSum = await _context.PaymentNotes
            .Where(p => p.BillId == bill.Id)
            .SumAsync(p => p.Amount);

        var debt = bill.Amount - payedSum;

        return debt;
    }
}

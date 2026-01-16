using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class BillRepository : IBillRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public BillRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<BillEntity> ApplyFilter(IQueryable<BillEntity> query, BillFilter filter)
    {
        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(b => filter.OrderIds.Contains(b.OrderId));

        return query;
    }

    public async Task<List<BillItem>> GetPaged(BillFilter filter, CancellationToken ct)
    {
        var query = _context.Bills.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "status" => filter.IsDescending
                ? query.OrderByDescending(a => a.Status == null 
                    ? string.Empty
                    : a.Status.Name)
                : query.OrderBy(a => a.Status == null
                    ? string.Empty
                    : a.Status.Name),
            "createat" => filter.IsDescending
                ? query.OrderByDescending(a => a.CreatedAt)
                : query.OrderBy(a => a.CreatedAt),
            "amount" => filter.IsDescending
                ? query.OrderByDescending(a => a.Amount)
                : query.OrderBy(a => a.Amount),
            "actualbilldate" => filter.IsDescending
                ? query.OrderByDescending(a => a.ActualBillDate)
                : query.OrderBy(a => a.ActualBillDate),

            _ => filter.IsDescending
                ? query.OrderByDescending(a => a.Id)
                : query.OrderBy(a => a.Id),
        };

        return await query
            .ProjectTo<BillItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(BillFilter filter, CancellationToken ct)
    {
        var query = _context.Bills.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<BillItem?> GetByOrderId(long orderId, CancellationToken ct)
    {
        var bill = await _context.Bills
            .AsNoTracking()
            .Where(b => b.OrderId == orderId)
            .ProjectTo<BillItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);

        return bill;
    }

    public async Task<long> Create(Bill bill, CancellationToken ct)
    {
        var billEntity = new BillEntity
        {
            OrderId = bill.OrderId,
            StatusId = (int)bill.StatusId,
            CreatedAt = bill.CreatedAt,
            Amount = bill.Amount,
            ActualBillDate = bill.ActualBillDate,
        };

        await _context.Bills.AddAsync(billEntity, ct);
        await _context.SaveChangesAsync(ct);

        return billEntity.Id;
    }

    public async Task<long> Update(long id, BillUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Bills.FirstOrDefaultAsync(b => b.Id == id, ct)
            ?? throw new NotFoundException("Bill not found");

        if (model.StatusId.HasValue) entity.StatusId = (int)model.StatusId.Value;
        if (model.Amount.HasValue) entity.Amount = model.Amount.Value;
        if (model.ActualBillDate.HasValue) entity.ActualBillDate = model.ActualBillDate.Value;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entity = await _context.Bills
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await _context.Bills
            .AsNoTracking()
            .AnyAsync(b => b.Id == id, ct);
    }

    public async Task<long> RecalculateAmount(long orderId, CancellationToken ct)
    {
        var bill = await _context.Bills.FirstOrDefaultAsync(b => b.OrderId == orderId, ct)
            ?? throw new NotFoundException("Bill not found");

        var newSetSum = await _context.PartSets
            .Where(b => b.OrderId == orderId)
            .SumAsync(b => b.SoldPrice * b.Quantity, ct);

        var newWorkInOrderSum = await _context.WorksInOrder
            .Where(b => b.OrderId == orderId)
            .SumAsync(b => (b.Worker != null)
                ? b.TimeSpent * b.Worker.HourlyRate
                : 0m, ct);

        bill.Amount = newSetSum + newWorkInOrderSum;

        await _context.SaveChangesAsync(ct);

        return bill.Id;
    }

    public async Task<decimal> RecalculateDebt(long Id, CancellationToken ct)
    {
        var bill = await _context.Bills.FirstOrDefaultAsync(b => b.Id == Id, ct)
            ?? throw new NotFoundException("Bill not found");

        var payedSum = await _context.PaymentNotes
            .Where(p => p.BillId == bill.Id)
            .SumAsync(p => p.Amount, ct);

        var debt = bill.Amount - payedSum;

        if (debt <= 0)
            bill.StatusId = (int)BillStatusEnum.Paid;
        else if (debt == bill.Amount)
            bill.StatusId = (int)BillStatusEnum.Unpaid;
        else
            bill.StatusId = (int)BillStatusEnum.PartiallyPaid;

        await _context.SaveChangesAsync(ct);

        return debt;
    }
}

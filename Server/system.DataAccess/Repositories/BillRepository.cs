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

    public async Task<List<Bill>> Get()
    {
        var billEntities = await _context.Bills
            .AsNoTracking()
            .ToListAsync();

        var bill = billEntities
            .Select(b => Bill.Create(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.CreateAt,
                b.Amount,
                b.ActualBillDate).bill)
            .ToList();

        return bill;
    }

    public async Task<List<Bill>> GetPaged(int page, int limit)
    {
        var billEntities = await _context.Bills
            .AsNoTracking()
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var bill = billEntities
            .Select(b => Bill.Create(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.CreateAt,
                b.Amount,
                b.ActualBillDate).bill)
            .ToList();

        return bill;
    }

    public async Task<int> GetCount()
    {
        return await _context.Bills.CountAsync();
    }

    public async Task<List<Bill>> GetByOrderId(List<int> orderIds)
    {
        var billEntities = await _context.Bills
            .AsNoTracking()
            .Where(b => orderIds.Contains(b.OrderId))
            .ToListAsync();

        var bill = billEntities
            .Select(b => Bill.Create(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.Date,
                b.Amount,
                b.ActualBillDate).bill)
            .ToList();

        return bill;
    }

    public async Task<List<Bill>> GetPagedByOrderId(List<int> orderIds, int page, int limit)
    {
        var billEntities = await _context.Bills
            .AsNoTracking()
            .Where(b => orderIds.Contains(b.OrderId))
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var bill = billEntities
            .Select(b => Bill.Create(
                b.Id,
                b.OrderId,
                b.StatusId,
                b.Date,
                b.Amount,
                b.ActualBillDate).bill)
            .ToList();

        return bill;
    }

    public async Task<int> GetCountByOrderId(List<int> orderIds)
    {
        return await _context.Bills.Where(b => orderIds.Contains(b.OrderId)).CountAsync();
    }

    public async Task<int> Create(Bill bill)
    {
        var (_, error) = Bill.Create(
            bill.Id,
            bill.OrderId,
            bill.StatusId,
            bill.Date,
            bill.Amount,
            bill.ActualBillDate);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Client: {error}");

        var billEntity = new BillEntity
        {
            Id = bill.Id,
            OrderId = bill.OrderId,
            StatusId = bill.StatusId,
            CreateAt = bill.Date,
            Amount = bill.Amount,
            ActualBillDate = bill.ActualBillDate,
        };

        await _context.Bills.AddAsync(billEntity);
        await _context.SaveChangesAsync();

        return billEntity.Id;
    }
}

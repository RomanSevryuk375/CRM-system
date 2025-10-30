using CRMSystem.Core.Models;
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
                b.Date,
                b.Amount,
                b.ActualBillDate).bill)
            .ToList();

        return bill;
    }

    public async Task<int> Update(
        int id,
        int? orderId,
        int? statusId,
        DateTime? date,
        decimal? amount,
        DateTime? actualBillDate)
    {
        var bill = await _context.Bills.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Bill not found");

        if (orderId.HasValue)
            bill.OrderId = orderId.Value;
        if (statusId.HasValue)
            bill.StatusId = statusId.Value;
        if (date.HasValue)
            bill.Date = date.Value;
        if (amount.HasValue)
            bill.Amount = amount.Value;
        if (actualBillDate.HasValue)
            bill.ActualBillDate = actualBillDate.Value;

        await _context.SaveChangesAsync();

        return bill.Id;
    }
}

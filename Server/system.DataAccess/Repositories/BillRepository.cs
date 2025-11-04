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
}

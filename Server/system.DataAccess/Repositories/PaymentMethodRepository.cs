using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly SystemDbContext _context;

    public PaymentMethodRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentMethodItem>> Get()
    {
        var query = _context.PaymentMethods.AsNoTracking();

        var projection = query.Select(p => new PaymentMethodItem(
            p.Id,
            p.Name));

        return await projection.ToListAsync();
    }
}

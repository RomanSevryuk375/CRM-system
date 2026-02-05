using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PaymentMethodRepository(
    SystemDbContext context,
    IMapper mapper) : IPaymentMethodRepository
{
    public async Task<List<PaymentMethodItem>> Get(CancellationToken ct)
    {
        return await context.PaymentMethods
            .AsNoTracking()
            .ProjectTo<PaymentMethodItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.PaymentMethods
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, ct);
    }
}

using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.Billing.Infrastructure.Persistence.Repositories;

internal sealed class PriceListRepository(BillingDbContext dbContext)
    : BaseRepository<PriceList, BillingDbContext, PriceListId>(dbContext), IPriceListRepository
{
    public override async Task<PriceList?> GetByIdAsync(
        PriceListId id,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}

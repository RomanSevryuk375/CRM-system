using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.Billing.Infrastructure.Persistence.Repositories;

internal sealed class BillRepository(BillingDbContext dbContext)
    : BaseRepository<Bill, BillingDbContext, BillId>(dbContext), IBillRepository
{
    public override async Task<Bill?> GetByIdAsync(
        BillId id,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.PaymentNotes)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}

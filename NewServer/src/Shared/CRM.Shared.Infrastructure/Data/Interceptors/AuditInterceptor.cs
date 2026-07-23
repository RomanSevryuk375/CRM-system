using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CRM.Shared.Infrastructure.Data.Interceptors;

public sealed class AuditInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        foreach (EntityEntry<IAuditable> entry in eventData.Context.ChangeTracker.Entries<IAuditable>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
                entry.Property(x => x.CreatedBy).CurrentValue = userContext.UserId;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
                entry.Property(x => x.UpdatedBy).CurrentValue = userContext.UserId;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CRM.Shared.Infrastructure.Data.Interceptors;

public sealed class SoftDeleteInterceptor(IUserContext userContext) : SaveChangesInterceptor
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

        foreach (EntityEntry<ISoftDeletable> entry in eventData.Context.ChangeTracker.Entries<ISoftDeletable>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;

                entry.Property(x => x.IsDeleted).CurrentValue = true;
                entry.Property(x => x.DeletedAt).CurrentValue = DateTimeOffset.UtcNow;
                entry.Property(x => x.DeletedBy).CurrentValue = userContext.UserId;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
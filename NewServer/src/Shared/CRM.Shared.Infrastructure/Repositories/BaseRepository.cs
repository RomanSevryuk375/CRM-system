using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using Microsoft.EntityFrameworkCore;

namespace CRM.Shared.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TDbContext, TId>(TDbContext dbContext)
    : IRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext = dbContext;
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken);
    }

    public async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(e => EF.Property<TId>(e, "Id").Equals(id), cancellationToken);
    }

    public async Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}
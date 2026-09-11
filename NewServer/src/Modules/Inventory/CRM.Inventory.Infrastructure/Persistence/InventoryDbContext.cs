using CRM.Inventory.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using CRM.Shared.Infrastructure.Data.InboxMessages;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Inventory.Infrastructure.Persistence;

internal class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public const string Schema = "inventory";

    public DbSet<PartCategory> PartCategories => Set<PartCategory>();
    public DbSet<Part> Parts => Set<Part>();
    public DbSet<StorageCell> StorageCells => Set<StorageCell>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Supply> Supplies => Set<Supply>();
    public DbSet<SupplyItem> SupplyItems => Set<SupplyItem>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddSharedValueConverters();
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

        modelBuilder.Entity<OutboxMessage>().ConfigureOutboxMessage(Schema);
        modelBuilder.Entity<InboxMessage>().ConfigureInboxMessage(Schema);

        base.OnModelCreating(modelBuilder);
    }
}

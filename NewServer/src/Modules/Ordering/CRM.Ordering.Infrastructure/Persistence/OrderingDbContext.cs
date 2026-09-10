using CRM.Ordering.Domain.Entities;
using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Domain.Entities.VehicleInspections;
using CRM.Shared.Infrastructure.Data.InboxMessages;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Ordering.Infrastructure.Persistence;

internal class OrderingDbContext(DbContextOptions<OrderingDbContext> options) : DbContext(options)
{
    public const string Schema = "ordering";

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderWork> OrderWorks => Set<OrderWork>();
    public DbSet<OrderPart> OrderParts => Set<OrderPart>();
    public DbSet<OrderGuarantee> OrderGuarantees => Set<OrderGuarantee>();
    public DbSet<WorkProposal> WorkProposals => Set<WorkProposal>();
    public DbSet<VehicleInspection> VehicleInspections => Set<VehicleInspection>();
    public DbSet<VehicleInspectionImage> VehicleInspectionImages => Set<VehicleInspectionImage>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<ServiceCatalogItem> ServiceCatalogItems => Set<ServiceCatalogItem>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

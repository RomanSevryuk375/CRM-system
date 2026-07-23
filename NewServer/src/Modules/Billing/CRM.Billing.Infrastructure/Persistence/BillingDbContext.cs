using CRM.Billing.Domain.Entities;
using CRM.Shared.Infrastructure.Data.InboxMessages;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Billing.Infrastructure.Persistence;

internal class BillingDbContext(DbContextOptions<BillingDbContext> options) : DbContext(options)
{
    public const string Schema = "billing";

    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<PaymentNote> PaymentNotes => Set<PaymentNote>();
    public DbSet<PriceList> PriceLists => Set<PriceList>();
    public DbSet<PriceListItem> PriceListItems => Set<PriceListItem>();
    public DbSet<Tax> Taxes => Set<Tax>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

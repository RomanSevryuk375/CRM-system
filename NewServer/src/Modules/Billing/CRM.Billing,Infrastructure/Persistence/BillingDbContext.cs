using CRM.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Billing.Infrastructure.Persistence;

internal class BillingDbContext(DbContextOptions<BillingDbContext> options) : DbContext(options)
{
    public const string Schema = "billing";

    public DbSet<Bill> Bills { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<PaymentNote> PaymentNotes { get; set; }
    public DbSet<PriceList> PriceLists { get; set; }
    public DbSet<PriceListItem> PriceListItems { get; set; }
    public DbSet<Tax> Taxes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

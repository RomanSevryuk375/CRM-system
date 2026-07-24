using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.Billing.Infrastructure.Persistence;

internal sealed class BillingDbContextFactory : IDesignTimeDbContextFactory<BillingDbContext>
{
    public BillingDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<BillingDbContext> builder = new();

        const string connectionString =
            "Host=localhost;Port=5438;Database=AUTOService;Username=postgres;Password=postgres";

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", BillingDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new BillingDbContext(builder.Options);
    }
}
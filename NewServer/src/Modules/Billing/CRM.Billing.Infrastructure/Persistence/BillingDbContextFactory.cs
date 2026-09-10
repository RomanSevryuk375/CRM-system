using CRM.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.Billing.Infrastructure.Persistence;

internal sealed class BillingDbContextFactory : IDesignTimeDbContextFactory<BillingDbContext>
{
    public BillingDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<BillingDbContext> builder = new();

        string connectionString = ConnectionStringHelper.GetConnectionString(connectionName: nameof(BillingDbContext));

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", BillingDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new BillingDbContext(builder.Options);
    }
}
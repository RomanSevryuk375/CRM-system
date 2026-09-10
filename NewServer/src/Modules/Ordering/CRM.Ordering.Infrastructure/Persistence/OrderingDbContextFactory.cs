using CRM.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.Ordering.Infrastructure.Persistence;

internal sealed class OrderingDbContextFactory : IDesignTimeDbContextFactory<OrderingDbContext>
{
    public OrderingDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<OrderingDbContext> builder = new();

        string connectionString = ConnectionStringHelper.GetConnectionString(connectionName: nameof(OrderingDbContext));

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", OrderingDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new OrderingDbContext(builder.Options);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.Ordering.Infrastructure.Persistence;

internal sealed class OrderingDbContextFactory : IDesignTimeDbContextFactory<OrderingDbContext>
{
    public OrderingDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<OrderingDbContext> builder = new();

        const string connectionString =
            "Host=localhost;Port=5438;Database=AUTOService;Username=postgres;Password=postgres";

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", OrderingDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new OrderingDbContext(builder.Options);
    }
}

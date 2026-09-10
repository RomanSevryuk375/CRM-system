using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.Customers.Infrastructure.Persistence;

internal sealed class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomersDbContext>
{
    public CustomersDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<CustomersDbContext> builder = new();

        const string connectionString =
            "Host=localhost;Port=5438;Database=AUTOService;Username=postgres;Password=postgres";

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", CustomersDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new CustomersDbContext(builder.Options);
    }
}

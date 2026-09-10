using CRM.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.Customers.Infrastructure.Persistence;

internal sealed class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomersDbContext>
{
    public CustomersDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<CustomersDbContext> builder = new();

        string connectionString = ConnectionStringHelper.GetConnectionString(connectionName: nameof(CustomersDbContext));

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", CustomersDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new CustomersDbContext(builder.Options);
    }
}

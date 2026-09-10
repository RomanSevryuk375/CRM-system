using CRM.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.HR.Infrastructure.Persistence;

internal sealed class HrDbContextFactory : IDesignTimeDbContextFactory<HrDbContext>
{
    public HrDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<HrDbContext> builder = new();

        string connectionString = ConnectionStringHelper.GetConnectionString(connectionName: nameof(HrDbContext));

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", HrDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new HrDbContext(builder.Options);
    }
}

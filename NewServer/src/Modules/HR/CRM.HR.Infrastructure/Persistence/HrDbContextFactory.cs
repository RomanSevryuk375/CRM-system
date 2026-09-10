using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.HR.Infrastructure.Persistence;

internal sealed class HrDbContextFactory : IDesignTimeDbContextFactory<HrDbContext>
{
    public HrDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<HrDbContext> builder = new();

        const string connectionString =
            "Host=localhost;Port=5438;Database=AUTOService;Username=postgres;Password=postgres";

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", HrDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new HrDbContext(builder.Options);
    }
}

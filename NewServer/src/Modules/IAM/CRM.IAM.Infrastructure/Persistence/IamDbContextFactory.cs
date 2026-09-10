using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.IAM.Infrastructure.Persistence;

internal sealed class IamDbContextFactory : IDesignTimeDbContextFactory<IamDbContext>
{
    public IamDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<IamDbContext> builder = new();

        const string connectionString =
            "Host=localhost;Port=5438;Database=AUTOService;Username=postgres;Password=postgres";

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", IamDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new IamDbContext(builder.Options);
    }
}

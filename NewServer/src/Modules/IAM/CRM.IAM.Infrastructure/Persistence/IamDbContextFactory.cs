using CRM.Shared.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CRM.IAM.Infrastructure.Persistence;

internal sealed class IamDbContextFactory : IDesignTimeDbContextFactory<IamDbContext>
{
    public IamDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<IamDbContext> builder = new();

        string connectionString = ConnectionStringHelper.GetConnectionString(connectionName: nameof(IamDbContext));

        builder.UseNpgsql(connectionString, b =>
            b.MigrationsHistoryTable("__EFMigrationsHistory", IamDbContext.Schema))
            .UseSnakeCaseNamingConvention();

        return new IamDbContext(builder.Options);
    }
}

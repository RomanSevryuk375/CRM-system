using CRM.Billing.Domain.Interfaces;
using CRM.Billing.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CRM.Billing.Infrastructure.Factories;

internal sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        string? connectionString = configuration.GetConnectionString(nameof(BillingDbContext));
        return new NpgsqlConnection(connectionString);
    }
}

using System.Data;
using CRM.Billing.Domain.Interfaces;
using CRM.Billing.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CRM.Billing.Infrastructure.Factories;

internal sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        string connectionString = ConnectionStringHelper.GetConnectionString(configuration, nameof(BillingDbContext));
        return new NpgsqlConnection(connectionString);
    }
}

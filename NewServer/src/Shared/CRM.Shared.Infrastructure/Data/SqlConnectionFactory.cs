using System.Data;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CRM.Shared.Infrastructure.Data;

public sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        string connectionString = ConnectionStringHelper.GetConnectionString(configuration);
        return new NpgsqlConnection(connectionString);
    }
}
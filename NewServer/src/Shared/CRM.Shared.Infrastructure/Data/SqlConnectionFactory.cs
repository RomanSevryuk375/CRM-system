using CRM.Shared.Abstractions.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CRM.Shared.Infrastructure.Data;

public sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        string? connectionString = configuration.GetConnectionString("Database");
        return new NpgsqlConnection(connectionString);
    }
}
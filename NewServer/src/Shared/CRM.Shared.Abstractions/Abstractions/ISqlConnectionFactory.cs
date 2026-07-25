using System.Data;

namespace CRM.Shared.Abstractions.Abstractions;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
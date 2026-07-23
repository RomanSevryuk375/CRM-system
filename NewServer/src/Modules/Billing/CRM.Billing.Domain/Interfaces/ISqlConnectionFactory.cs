using System.Data;
namespace CRM.Billing.Domain.Interfaces;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Infrastructure.Data;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.Taxes.Queries.GetTaxById;

public sealed class GetTaxByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetTaxByIdQuery, TaxDetailsDto?>
{
    public async Task<TaxDetailsDto?> Handle(GetTaxByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
              id AS Id, 
              name AS Name, 
              rate AS Rate, 
              type AS TypeId, 
              created_at AS CreatedAt,
              updated_at AS UpdatedAt
            FROM billing.taxes
            WHERE id = @TaxId AND is_deleted = false
            LIMIT 1;
            """;

        return await connection.QuerySingleOrDefaultAsync<TaxDetailsDto>(
            new CommandDefinition(sql, new { request.TaxId }, cancellationToken: cancellationToken));
    }
}
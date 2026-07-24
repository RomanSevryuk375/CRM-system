using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Infrastructure.Data;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.Taxes.Queries.GetTaxesList;

public sealed class GetTaxesListQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetTaxesListQuery, IReadOnlyList<TaxListItemDto>>
{
    public async Task<IReadOnlyList<TaxListItemDto>> Handle(
        GetTaxesListQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
              id AS Id, 
              name AS Name, 
              rate AS Rate, 
              type AS TypeId
            FROM billing.taxes
            WHERE is_deleted = false
            ORDER BY name ASC;
            """;

        IEnumerable<TaxListItemDto> taxes = await connection.QueryAsync<TaxListItemDto>(
            new CommandDefinition(sql, cancellationToken: cancellationToken));

        return taxes.ToList().AsReadOnly();
    }
}
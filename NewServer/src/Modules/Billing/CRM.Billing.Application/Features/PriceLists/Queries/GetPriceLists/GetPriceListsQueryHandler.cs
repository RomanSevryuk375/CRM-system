using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.PriceLists.Queries.GetPriceLists;

public sealed class GetPriceListsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetPriceListsQuery, IReadOnlyList<PriceListSummaryDto>>
{
    public async Task<IReadOnlyList<PriceListSummaryDto>> Handle(
        GetPriceListsQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
              id AS Id, 
              name AS Name, 
              valid_from AS ValidFrom, 
              valid_to AS ValidTo, 
              is_default AS IsDefault, 
              base_hourly_rate AS BaseHourlyRate, 
              created_at AS CreatedAt
            FROM billing.price_lists
            WHERE is_deleted = false
            ORDER BY valid_from DESC, created_at DESC
            LIMIT @Limit OFFSET @Offset;
            """;

        IEnumerable<PriceListSummaryDto> priceLists = await connection.QueryAsync<PriceListSummaryDto>(
            new CommandDefinition(sql, request, cancellationToken: cancellationToken));

        return priceLists.ToList().AsReadOnly();
    }
}
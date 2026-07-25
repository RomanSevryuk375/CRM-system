using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.PriceLists.Queries.GetActivePriceList;

public sealed class GetActivePriceListQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetActivePriceListQuery, ActivePriceListDto?>
{
    public async Task<ActivePriceListDto?> Handle(
        GetActivePriceListQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
              id AS Id, 
              name AS Name, 
              base_hourly_rate AS BaseHourlyRate
            FROM billing.price_lists
            WHERE is_default = true 
              AND is_deleted = false
              AND valid_from <= @Today 
              AND (valid_to IS NULL OR valid_to >= @Today)
            LIMIT 1;
            """;

        ActivePriceListDto? priceList = await connection.QuerySingleOrDefaultAsync<ActivePriceListDto>(
            new CommandDefinition(sql, new { request.Today }, cancellationToken: cancellationToken));
        if (priceList is null)
        {
            return null;
        }

        const string itemsSql = """
            SELECT 
              job_id AS JobId, 
              fixed_price AS FixedPrice
            FROM billing.price_list_items
            WHERE price_list_id = @PriceListId;
            """;

        IEnumerable<ActiveFixedPriceItemDto> items = await connection.QueryAsync<ActiveFixedPriceItemDto>(
            new CommandDefinition(itemsSql, new { PriceListId = priceList.Id }, cancellationToken: cancellationToken));

        return priceList with { FixedPrices = items.ToList().AsReadOnly() };
    }
}
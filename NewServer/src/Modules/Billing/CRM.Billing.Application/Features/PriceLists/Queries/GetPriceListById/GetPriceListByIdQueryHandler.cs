using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.PriceLists.Queries.GetPriceListById;

public sealed class GetPriceListByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetPriceListByIdQuery, PriceListDetailsDto?>
{
    public async Task<PriceListDetailsDto?> Handle(
        GetPriceListByIdQuery request, CancellationToken cancellationToken)
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
            WHERE id = @PriceListId AND is_deleted = false;

            SELECT 
              id AS Id, 
              job_id AS JobId, 
              fixed_price AS FixedPrice
            FROM billing.price_list_items
            WHERE price_list_id = @PriceListId;
            """;

        using SqlMapper.GridReader multi = await connection.QueryMultipleAsync(
            new CommandDefinition(sql, new { request.PriceListId }, cancellationToken: cancellationToken));

        PriceListDetailsDto? priceList = await multi.ReadSingleOrDefaultAsync<PriceListDetailsDto>();
        if (priceList is null)
        {
            return null;
        }

        IEnumerable<FixedPriceItemDto> fixedPrices = await multi.ReadAsync<FixedPriceItemDto>();

        return priceList with { FixedPrices = fixedPrices.ToList().AsReadOnly() };
    }
}
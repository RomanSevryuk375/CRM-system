using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using Dapper;
using System.Data;
using System.Text;

namespace CRM.Billing.Application.Features.Bills.Queries.GetPagedBillы;

public sealed class GetPagedBillsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetPagedBillsQuery, IReadOnlyList<BillListItemDto>>
{
    public async Task<IReadOnlyList<BillListItemDto>> Handle(
        GetPagedBillsQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        StringBuilder sqlBuilder = new("""
            SELECT 
                id AS Id, 
                order_id AS OrderId, 
                status AS StatusId, 
                amount AS Amount, 
                created_at AS CreatedAt
            FROM billing.bills
            WHERE is_deleted = false
            """);

        if (request.OrderId.HasValue)
        {
            sqlBuilder.AppendLine(" AND order_id = @OrderId");
        }

        if (request.StatusId.HasValue)
        {
            sqlBuilder.AppendLine(" AND status = @StatusId");
        }

        sqlBuilder.AppendLine(" ORDER BY created_at DESC LIMIT @Limit OFFSET @Offset;");

        IEnumerable<BillListItemDto> bills = await connection.QueryAsync<BillListItemDto>(
            new CommandDefinition(sqlBuilder.ToString(), request, cancellationToken: cancellationToken));

        return bills.ToList().AsReadOnly();
    }
}
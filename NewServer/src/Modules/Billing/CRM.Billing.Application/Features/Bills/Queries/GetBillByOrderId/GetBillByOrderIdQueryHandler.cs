using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Infrastructure.Data;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.Bills.Queries.GetBillByOrderId;

public sealed class GetBillByOrderIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetBillByOrderIdQuery, BillSummaryDto?>
{
    public async Task<BillSummaryDto?> Handle(GetBillByOrderIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
              b.id AS Id, 
              b.order_id AS OrderId, 
              b.status AS StatusId, 
              b.amount AS Amount,
              COALESCE((
                SELECT SUM(pn.amount) 
                FROM billing.payment_notes pn 
                WHERE pn.bill_id = b.id AND pn.is_deleted = false
              ), 0) AS PaidAmount
            FROM billing.bills b
            WHERE b.order_id = @OrderId AND b.is_deleted = false
            LIMIT 1;
            """;

        BillSummaryDto? billSummary = await connection.QuerySingleOrDefaultAsync<BillSummaryDto>(
            new CommandDefinition(sql, new { request.OrderId }, cancellationToken: cancellationToken));

        return billSummary;
    }
}
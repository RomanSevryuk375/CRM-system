using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.Expenses.Queries.GetExpensesSummary;

internal sealed class GetExpensesSummaryQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetExpensesSummaryQuery, IReadOnlyList<ExpenseSummaryDto>>
{
    public async Task<IReadOnlyList<ExpenseSummaryDto>> Handle(
        GetExpensesSummaryQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                type AS TypeId, 
                SUM(amount) AS TotalAmount
            FROM billing.expenses
            WHERE is_deleted = false 
              AND date >= @DateFrom 
              AND date <= @DateTo
            GROUP BY type
            ORDER BY TotalAmount DESC;
            """;

        IEnumerable<ExpenseSummaryDto> summary = await connection.QueryAsync<ExpenseSummaryDto>(
            new CommandDefinition(sql, request, cancellationToken: cancellationToken));

        return summary.ToList().AsReadOnly();
    }
}
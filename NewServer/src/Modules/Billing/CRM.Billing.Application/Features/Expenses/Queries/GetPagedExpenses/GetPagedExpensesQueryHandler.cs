using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Infrastructure.Data;
using Dapper;
using System.Data;
using System.Text;

namespace CRM.Billing.Application.Features.Expenses.Queries.GetPagedExpenses;

public sealed class GetPagedExpensesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetPagedExpensesQuery, IReadOnlyList<ExpenseListItemDto>>
{
    public async Task<IReadOnlyList<ExpenseListItemDto>> Handle(
        GetPagedExpensesQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        StringBuilder sqlBuilder = new("""
            SELECT 
                id AS Id, 
                date AS Date, 
                category AS Category, 
                type AS TypeId, 
                amount AS Amount, 
                reference_id AS ReferenceId
            FROM billing.expenses
            WHERE is_deleted = false
            """);

        if (request.DateFrom.HasValue)
        {
            sqlBuilder.AppendLine(" AND date >= @DateFrom");
        }

        if (request.DateTo.HasValue)
        {
            sqlBuilder.AppendLine(" AND date <= @DateTo");
        }

        if (request.TypeId.HasValue)
        {
            sqlBuilder.AppendLine(" AND type = @TypeId");
        }

        if (request.TaxId.HasValue)
        {
            sqlBuilder.AppendLine(" AND tax_id = @TaxId");
        }

        if (request.ReferenceId.HasValue)
        {
            sqlBuilder.AppendLine(" AND reference_id = @ReferenceId");
        }

        sqlBuilder.AppendLine(" ORDER BY date DESC, created_at DESC LIMIT @Limit OFFSET @Offset;");

        IEnumerable<ExpenseListItemDto> expenses = await connection.QueryAsync<ExpenseListItemDto>(
            new CommandDefinition(sqlBuilder.ToString(), request, cancellationToken: cancellationToken));

        return expenses.ToList().AsReadOnly();
    }
}
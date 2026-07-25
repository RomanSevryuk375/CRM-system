using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.Expenses.Queries.GetExpenseById;

public sealed class GetExpenseByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetExpenseByIdQuery, ExpenseDetailsDto?>
{
    public async Task<ExpenseDetailsDto?> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
              id AS Id, 
              date AS Date, 
              category AS Category, 
              description AS Description, 
              type AS TypeId, 
              amount AS Amount, 
              tax_id AS TaxId, 
              reference_id AS ReferenceId, 
              created_at AS CreatedAt
            FROM billing.expenses
            WHERE id = @ExpenseId AND is_deleted = false
            LIMIT 1;
            """;

        return await connection.QuerySingleOrDefaultAsync<ExpenseDetailsDto>(
            new CommandDefinition(sql, new { request.ExpenseId }, cancellationToken: cancellationToken));
    }
}
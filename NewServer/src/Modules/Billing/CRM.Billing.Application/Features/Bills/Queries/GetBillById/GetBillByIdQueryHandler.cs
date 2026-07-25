using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using Dapper;
using System.Data;

namespace CRM.Billing.Application.Features.Bills.Queries.GetBillById;

public sealed class GetBillByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetBillByIdQuery, BillDetailsDto?>
{
    public async Task<BillDetailsDto?> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
              id AS Id, 
              order_id AS OrderId, 
              status AS StatusId, 
              amount AS Amount, 
              actual_bill_date AS ActualBillDate, 
              created_at AS CreatedAt
            FROM billing.bills
            WHERE id = @BillId AND is_deleted = false;

            SELECT 
              id AS Id, 
              amount AS Amount, 
              method AS MethodId, 
              date AS Date
            FROM billing.payment_notes
            WHERE bill_id = @BillId AND is_deleted = false
            ORDER BY date DESC;
            """;

        using SqlMapper.GridReader multi = await connection.QueryMultipleAsync(
            new CommandDefinition(sql, new { request.BillId }, cancellationToken: cancellationToken));

        BillDetailsDto? bill = await multi.ReadSingleOrDefaultAsync<BillDetailsDto>();
        if (bill is null)
        {
            return null;
        }

        IEnumerable<PaymentNoteDto> payments = await multi.ReadAsync<PaymentNoteDto>();

        return bill with { Payments = payments.ToList().AsReadOnly() };
    }
}
using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;

namespace CRM.Billing.Domain.Interfaces;

public interface IBillRepository : IRepository<Bill, BillId>
{
    new Task<Bill?> GetByIdAsync(
        BillId id,
        CancellationToken cancellationToken = default);
}

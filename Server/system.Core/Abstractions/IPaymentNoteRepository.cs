using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using Shared.Enums;

namespace CRMSystem.Core.Abstractions;

public interface IPaymentNoteRepository
{
    Task<long> Create(PaymentNote paymentNote, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(PaymentNoteFilter fIlter, CancellationToken ct);
    Task<List<PaymentNoteItem>> GetPaged(PaymentNoteFilter fIlter, CancellationToken ct);
    Task<long> Update(long id, PaymentMethodEnum? method, CancellationToken ct);
}
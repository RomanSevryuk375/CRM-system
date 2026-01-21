using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.Core.Models;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IPaymentNoteService
{
    Task<long> CreatePaymentNote(PaymentNote paymentNote, CancellationToken ct);
    Task<long> DeletePaymentNote(long id, CancellationToken ct);
    Task<int> GetCountPaymentNotes(PaymentNoteFilter filter, CancellationToken ct);
    Task<List<PaymentNoteItem>> GetPagedPaymentNotes(PaymentNoteFilter filter, CancellationToken ct);
    Task<long> UpratePaymentNote(long id, PaymentMethodEnum? method, CancellationToken ct);
}
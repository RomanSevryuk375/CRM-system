using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPaymentNoteService
{
    Task<long> CreatePaymentNote(PaymentNote paymentNote);
    Task<long> DeletePaymentNote(long id);
    Task<int> GetCountPaymentNotes(PaymentNoteFilter filter);
    Task<List<PaymentNoteItem>> GetPagedPaymentNotes(PaymentNoteFilter filter);
    Task<long> UpratePaymentNote(long id, PaymentMethodEnum? method);
}
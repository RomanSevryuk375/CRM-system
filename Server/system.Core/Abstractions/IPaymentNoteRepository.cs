using CRMSystem.Core.DTOs.PaymentNote;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IPaymentNoteRepository
    {
        Task<long> Create(PaymentNote paymentNote);
        Task<long> Delete(long id);
        Task<int> GetCount(PaymentNoteFilter fIlter);
        Task<List<PaymentNoteItem>> GetPaged(PaymentNoteFilter fIlter);
        Task<long> Update(long id, PaymentMethodEnum? method);
    }
}
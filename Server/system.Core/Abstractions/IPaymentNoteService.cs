using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IPaymentNoteService
    {
        Task<List<PaymentNote>> GetPagedPaymentNote(int page, int limit);
        Task<int> GetCountPaymentNote();
        Task<List<PaymentNote>> GetPagedUserPaymentNote(int userId, int page, int limit);
        Task<int> GetCountUserPaymentNote(int userId);
        Task<int> CreatePaymentNote(PaymentNote paymentNote);
        Task<int> DeletePaymentNote(int id);
        Task<int> UpdatePaymentNote(int id, int? billId, DateTime? date, decimal? amount, string method);
    }
}
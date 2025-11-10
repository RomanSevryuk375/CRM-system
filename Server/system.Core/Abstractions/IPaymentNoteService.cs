using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IPaymentNoteService
    {
        Task<List<PaymentNote>> GetPaymentNote();
        Task<List<PaymentNote>> GetUserNote(int userId);
        Task<int> CreatePaymentNote(PaymentNote paymentNote);
        Task<int> DeletePaymentNote(int id);
        Task<int> UpdatePaymentNote(int id, int? billId, DateTime? date, decimal? amount, string method);
    }
}
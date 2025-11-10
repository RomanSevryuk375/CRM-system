using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IPaymentNoteRepository
    {
        Task<int> Create(PaymentNote paymentNote);
        Task<int> Delete(int id);
        Task<List<PaymentNote>> Get();
        Task<List<PaymentNote>> GetByBillId(List<int> billIds);
        Task<int> Update(int id, int? billId, DateTime? date, decimal? amount, string method);
    }
}
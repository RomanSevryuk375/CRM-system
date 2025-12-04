using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IPaymentNoteRepository
    {
        Task<List<PaymentNote>> Get();
        Task<List<PaymentNote>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<List<PaymentNote>> GetByBillId(List<int> billIds);
        Task<List<PaymentNote>> GetPagedByBillId(List<int> billIds, int page, int limit);
        Task<int> GetCountByBillId(List<int> billIds);
        Task<int> Create(PaymentNote paymentNote);
        Task<int> Delete(int id);
        Task<int> Update(int id, int? billId, DateTime? date, decimal? amount, string? method);
    }
}
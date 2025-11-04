using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class PaymentNoteService : IPaymentNoteService
{
    private readonly IPaymentNoteRepository _paymentNoteRepository;

    public PaymentNoteService(IPaymentNoteRepository paymentNoteRepository)
    {
        _paymentNoteRepository = paymentNoteRepository;
    }

    public async Task<List<PaymentNote>> GetPaymentNote()
    {
        return await _paymentNoteRepository.Get();
    }

    public async Task<int> CreatePaymentNote(PaymentNote paymentNote)
    {
        return await _paymentNoteRepository.Create(paymentNote);
    }

    public async Task<int> UpdatePaymentNote(int id, int? billId, DateTime? date, decimal? amount, string method)
    {
        return await _paymentNoteRepository.Update(id, billId, date, amount, method);
    }

    public async Task<int> DeletePaymentNote(int id)
    {
        return await _paymentNoteRepository.Delete(id);
    }
}

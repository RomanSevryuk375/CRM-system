using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PaymentNoteRepository : IPaymentNoteRepository
{
    private readonly SystemDbContext _context;

    public PaymentNoteRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentNote>> Get()
    {
        var paymentEntities = await _context.PaymentNotes
            .AsNoTracking()
            .ToListAsync();

        var paymentNote = paymentEntities
            .Select(pn => PaymentNote.Create(
                pn.Id,
                pn.BillId,
                pn.Date,
                pn.Amount,
                pn.Method).paymentNote)
            .ToList();

        return paymentNote;
    }

    public async Task<int> GetCount()
    {
        return await _context.PaymentNotes.CountAsync();
    }

    public async Task<List<PaymentNote>> GetByBillId(List<int> billIds)
    {
        var paymentEntities = await _context.PaymentNotes
           .AsNoTracking()
           .Where(p => billIds.Contains(p.BillId))
           .ToListAsync();

        var paymentNote = paymentEntities
            .Select(pn => PaymentNote.Create(
                pn.Id,
                pn.BillId,
                pn.Date,
                pn.Amount,
                pn.Method).paymentNote)
            .ToList();

        return paymentNote;
    }

    public async Task<int> GetCountByBillId(List<int> billIds)
    {
        return await _context.PaymentNotes.Where(p => billIds.Contains(p.BillId)).CountAsync();
    }

    public async Task<int> Create(PaymentNote paymentNote)
    {
        var (_, error) = PaymentNote.Create(
            0,
            paymentNote.BillId,
            paymentNote.Date,
            paymentNote.Amount,
            paymentNote.Method);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Car: {error}");

        var paymentNoteEntites = new PaymentNoteEntity
        {
            BillId = paymentNote.BillId,
            Date = paymentNote.Date,
            Amount = paymentNote.Amount,
            Method = paymentNote.Method,
        };

        await _context.PaymentNotes.AddAsync(paymentNoteEntites);
        await _context.SaveChangesAsync();

        return paymentNote.Id;
    }

    public async Task<int> Update(int id, int? billId, DateTime? date, decimal? amount, string? method)
    {
        var paymentNote = await _context.PaymentNotes.FirstOrDefaultAsync(pn => pn.Id == id)
            ?? throw new Exception("Payment note not found");

        if (billId.HasValue)
            paymentNote.BillId = billId.Value;
        if (date.HasValue)
            paymentNote.Date = date.Value;
        if (amount.HasValue)
            paymentNote.Amount = amount.Value;
        if (!string.IsNullOrEmpty(method))
            paymentNote.Method = method;

        await _context.SaveChangesAsync();

        return paymentNote.Id;
    }

    public async Task<int> Delete(int id)
    {
        var paymentNote = await _context.PaymentNotes
            .Where(pn => pn.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

}

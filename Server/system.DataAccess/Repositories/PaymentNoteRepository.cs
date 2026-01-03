using CRMSystem.Core.DTOs.PaymentNote;
using CRMSystem.Core.Enums;
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

    private IQueryable<PaymentNoteEntity> ApplyFilter(IQueryable<PaymentNoteEntity> query, PaymentNoteFilter filter)
    {
        if (filter.BillIds != null && filter.BillIds.Any())
            query = query.Where(p => filter.BillIds.Contains(p.BillId));

        if (filter.MethodIds != null && filter.MethodIds.Any())
            query = query.Where(p => filter.MethodIds.Contains(p.MethodId));

        return query;
    }

    public async Task<List<PaymentNoteItem>> GetPaged(PaymentNoteFilter fIlter)
    {
        var query = _context.PaymentNotes.AsNoTracking();
        query = ApplyFilter(query, fIlter);

        query = fIlter.SortBy?.ToLower().Trim() switch
        {
            "bill" => fIlter.IsDescending
                ? query.OrderByDescending(p => p.BillId)
                : query.OrderBy(p => p.BillId),
            "date" => fIlter.IsDescending
                ? query.OrderByDescending(p => p.Date)
                : query.OrderBy(p => p.Date),
            "amount" => fIlter.IsDescending
                ? query.OrderByDescending(p => p.Amount)
                : query.OrderBy(p => p.Amount),
            "method" => fIlter.IsDescending
                ? query.OrderByDescending(p => p.Method == null
                    ? string.Empty
                    : p.Method.Name)
                : query.OrderBy(p => p.Method == null
                    ? string.Empty
                    : p.Method.Name),

            _ => fIlter.IsDescending
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id),
        };

        var projection = query.Select(p => new PaymentNoteItem(
            p.Id,
            p.BillId,
            p.Date,
            p.Amount,
            p.Method == null
                    ? string.Empty
                    : p.Method.Name));

        return await projection
            .Skip((fIlter.Page - 1) * fIlter.Limit)
            .Take(fIlter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(PaymentNoteFilter fIlter)
    {
        var quety = _context.PaymentNotes.AsNoTracking();
        quety = ApplyFilter(quety, fIlter);
        return await quety.CountAsync();
    }

    public async Task<long> Create(PaymentNote paymentNote)
    {
        var paymentNoteEntity = new PaymentNoteEntity
        {
            BillId = paymentNote.BillId,
            Date = paymentNote.Date,
            Amount = paymentNote.Amount,
            MethodId = (int)paymentNote.MethodId,
        };

        await _context.AddAsync(paymentNoteEntity);
        await _context.SaveChangesAsync();

        return paymentNoteEntity.Id;
    }

    public async Task<long> Update(long id, PaymentMethodEnum? method)
    {
        var entity = await _context.PaymentNotes.FirstOrDefaultAsync(pn => pn.Id == id)
            ?? throw new Exception("Payment note not found");

        if (method.HasValue) entity.MethodId = (int)method.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var paymentNote = await _context.PaymentNotes
            .Where(pn => pn.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

}

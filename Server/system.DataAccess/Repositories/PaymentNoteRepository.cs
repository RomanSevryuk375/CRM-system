using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PaymentNoteRepository : IPaymentNoteRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public PaymentNoteRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private IQueryable<PaymentNoteEntity> ApplyFilter(IQueryable<PaymentNoteEntity> query, PaymentNoteFilter filter)
    {
        if (filter.BillIds != null && filter.BillIds.Any())
            query = query.Where(p => filter.BillIds.Contains(p.BillId));

        if (filter.MethodIds != null && filter.MethodIds.Any())
            query = query.Where(p => filter.MethodIds.Contains(p.MethodId));

        return query;
    }

    public async Task<List<PaymentNoteItem>> GetPaged(PaymentNoteFilter filter)
    {
        var query = _context.PaymentNotes.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "bill" => filter.IsDescending
                ? query.OrderByDescending(p => p.BillId)
                : query.OrderBy(p => p.BillId),
            "date" => filter.IsDescending
                ? query.OrderByDescending(p => p.Date)
                : query.OrderBy(p => p.Date),
            "amount" => filter.IsDescending
                ? query.OrderByDescending(p => p.Amount)
                : query.OrderBy(p => p.Amount),
            "method" => filter.IsDescending
                ? query.OrderByDescending(p => p.Method == null
                    ? string.Empty
                    : p.Method.Name)
                : query.OrderBy(p => p.Method == null
                    ? string.Empty
                    : p.Method.Name),

            _ => filter.IsDescending
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id),
        };

        return await query
            .ProjectTo<PaymentNoteItem>(_mapper.ConfigurationProvider)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(PaymentNoteFilter filter)
    {
        var quety = _context.PaymentNotes.AsNoTracking();
        quety = ApplyFilter(quety, filter);
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

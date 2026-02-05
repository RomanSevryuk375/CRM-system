using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class PaymentNoteRepository(
    SystemDbContext context,
    IMapper mapper) : IPaymentNoteRepository
{
    private static IQueryable<PaymentNoteEntity> ApplyFilter(IQueryable<PaymentNoteEntity> query, PaymentNoteFilter filter)
    {
        if (filter.BillIds != null && filter.BillIds.Any())
        {
            query = query.Where(p => filter.BillIds.Contains(p.BillId));
        }

        if (filter.MethodIds != null && filter.MethodIds.Any())
        {
            query = query.Where(p => filter.MethodIds.Contains(p.MethodId));
        }

        return query;
    }

    public async Task<List<PaymentNoteItem>> GetPaged(PaymentNoteFilter filter, CancellationToken ct)
    {
        var query = context.PaymentNotes.AsNoTracking();
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
            .ProjectTo<PaymentNoteItem>(mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(PaymentNoteFilter filter, CancellationToken ct)
    {
        var quety = context.PaymentNotes.AsNoTracking();

        quety = ApplyFilter(quety, filter);

        return await quety.CountAsync(ct);
    }

    public async Task<long> Create(PaymentNote paymentNote, CancellationToken ct)
    {
        var paymentNoteEntity = new PaymentNoteEntity
        {
            BillId = paymentNote.BillId,
            Date = paymentNote.Date,
            Amount = paymentNote.Amount,
            MethodId = (int)paymentNote.MethodId,
        };

        await context.AddAsync(paymentNoteEntity, ct);
        await context.SaveChangesAsync(ct);

        return paymentNoteEntity.Id;
    }

    public async Task<long> Update(long id, PaymentMethodEnum? method, CancellationToken ct)
    {
        var entity = await context.PaymentNotes.FirstOrDefaultAsync(pn => pn.Id == id, ct)
            ?? throw new NotFoundException("Payment note not found");

        if (method.HasValue)
        {
            entity.MethodId = (int)method.Value;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        await context.PaymentNotes
            .Where(pn => pn.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

}

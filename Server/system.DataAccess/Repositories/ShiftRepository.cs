using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class ShiftRepository(
    SystemDbContext context,
    IMapper mapper) : IShiftRepository
{
    public async Task<List<ShiftItem>> Get(CancellationToken ct)
    {
        return await context.Shifts
            .AsNoTracking()
            .ProjectTo<ShiftItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(Shift shift, CancellationToken ct)
    {
        var shiftEntity = new ShiftEntity
        {
            Name = shift.Name,
            StartAt = shift.StartAt,
            EndAt = shift.EndAt,
        };

        await context.Shifts.AddAsync(shiftEntity, ct);
        await context.SaveChangesAsync(ct);

        return shiftEntity.Id;
    }

    public async Task<int> Update(int id, ShiftUpdateModel model, CancellationToken ct)
    {
        var entity = await context.Shifts.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new NotFoundException("Shift note not found");

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            entity.Name = model.Name;
        }

        if (model.StartAt.HasValue)
        {
            entity.StartAt = model.StartAt.Value;
        }

        if (model.EndAt.HasValue)
        {
            entity.EndAt = model.EndAt.Value;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        await context.Shifts
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.Shifts
            .AsNoTracking()
            .AnyAsync(s => s.Id == id, ct);
    }

    public async Task<bool> HasOverLap(TimeOnly start, TimeOnly end, CancellationToken ct)
    {
        return await context.Shifts
            .AsNoTracking()
            .AnyAsync(s => s.StartAt == start 
                        && s.EndAt == end, ct);
    }
}

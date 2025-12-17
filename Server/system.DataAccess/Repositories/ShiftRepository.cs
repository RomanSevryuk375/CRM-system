using CRMSystem.Core.DTOs.Shift;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly SystemDbContext _context;

    public ShiftRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<ShiftItem>> Get()
    {
        var query = _context.Shifts.AsNoTracking();

        var projection = query.Select(s => new ShiftItem(
            s.Id,
            s.Name,
            s.StartAt,
            s.EndAt));

        return await projection.ToListAsync();
    }

    public async Task<int> Create(Shift shift)
    {
        var shiftEntity = new ShiftEntity
        {
            Name = shift.Name,
            StartAt = shift.StartAt,
            EndAt = shift.EndAt,
        };

        await _context.Shifts.AddAsync(shiftEntity);
        await _context.SaveChangesAsync();

        return shiftEntity.Id;
    }

    public async Task<int> Update(int id, ShiftUpdateModel model)
    {
        var entity = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new Exception("Shift note not found");

        if (!string.IsNullOrWhiteSpace(model.name)) entity.Name = model.name;
        if (model.startAt.HasValue) entity.StartAt = model.startAt.Value;
        if (model.endAt.HasValue) entity.EndAt = model.endAt.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.Shifts
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

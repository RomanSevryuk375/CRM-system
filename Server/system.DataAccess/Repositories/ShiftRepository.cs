using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public ShiftRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ShiftItem>> Get()
    {
        return await _context.Shifts
            .AsNoTracking()
            .ProjectTo<ShiftItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
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

        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (model.StartAt.HasValue) entity.StartAt = model.StartAt.Value;
        if (model.EndAt.HasValue) entity.EndAt = model.EndAt.Value;

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

    public async Task<bool> Exists(int id)
    {
        return await _context.Shifts
            .AsNoTracking()
            .AnyAsync(s => s.Id == id);
    }

    public async Task<bool> HasOverLap(TimeOnly start, TimeOnly end)
    {
        return await _context.Shifts
            .AsNoTracking()
            .AnyAsync(s => s.StartAt == start 
                        && s.EndAt == end);
    }
}

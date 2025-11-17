using CRMSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class RepairNoteRepositry : IRepairNoteRepositry
{
    private readonly SystemDbContext _context;

    public RepairNoteRepositry(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<RepairNote>> Get()
    {
        var repairNoteEntities = await _context.RepairHistories
            .AsNoTracking()
            .ToListAsync();

        var repairNote = repairNoteEntities
            .Select(rn => RepairNote.Create(
                rn.Id,
                rn.OrderId,
                rn.CarId,
                rn.WorkDate,
                rn.ServiceSum).repairHistory)
            .ToList();

        return repairNote;
    }

    public async Task<int> GetCount()
    {
        return await _context.RepairHistories.CountAsync();
    }

    public async Task<List<RepairNote>> GetByCarId(List<int> carIds)
    {
        var repairNoteEntity = await _context.RepairHistories
            .AsNoTracking()
            .Where(r => carIds.Contains(r.CarId))
            .ToListAsync();

        var repairNote = repairNoteEntity
            .Select(r => RepairNote.Create(
                r.Id,
                r.OrderId,
                r.CarId,
                r.WorkDate,
                r.ServiceSum).repairHistory)
            .ToList();

        return repairNote;
    }

    public async Task<int> GetCountByCarId(List<int> carIds)
    {
        return await _context.RepairHistories.Where(r => carIds.Contains(r.CarId)).CountAsync();
    }

    public async Task<List<RepairNote>> GetByOrderId(List<int> orderIds)
    {
        var repairNoteEntity = await _context.RepairHistories
            .AsNoTracking()
            .Where(r => orderIds.Contains(r.OrderId))
            .ToListAsync();

        var repairNote = repairNoteEntity
            .Select(r => RepairNote.Create(
                r.Id,
                r.OrderId,
                r.CarId,
                r.WorkDate,
                r.ServiceSum).repairHistory)
            .ToList();

        return repairNote;
    }

    public async Task<int> GetCountByOrderId(List<int> orderIds)
    {
        return await _context.RepairHistories.Where(r => orderIds.Contains(r.OrderId)).CountAsync();
    }
}

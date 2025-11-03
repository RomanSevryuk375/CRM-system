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
}

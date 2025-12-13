using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class AbsenceTypeRepository : IAbsenceTypeRepository
{
    private readonly SystemDbContext _context;

    public AbsenceTypeRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<AbcenseTypeItem>> GetPaged(int page, int pageSize)
    {
        var query = _context.AbsenceTypes
            .AsNoTracking();

        var projection = query
            .Select(x => new AbcenseTypeItem(
            x.Id,
            x.Name));

        return await projection
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCount()
    {
        return await _context.AbsenceTypes.CountAsync();
    }

    public async Task<int> Create(Core.Models.AbsenceType absenceType)
    {
        var entity = new AbsenceTypeEntity
        {
            Name = absenceType.Name
        };

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Update(int id, string name)
    {
        var entity = await _context.AbsenceTypes
            .FirstOrDefaultAsync(a => a.Id == id);

        if (entity == null) throw new Exception("Absences not found");

        if (!string.IsNullOrEmpty(name))
            entity.Name = name;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.AbsenceTypes
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

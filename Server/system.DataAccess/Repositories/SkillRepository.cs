using CRMSystem.Core.DTOs.Skill;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly SystemDbContext _context;

    public SkillRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<SkillEntity> ApplyFilter(IQueryable<SkillEntity> query, SkillFilter filter)
    {
        if (filter.workerIds != null && filter.workerIds.Any())
            query = query.Where(s => filter.workerIds.Contains(s.WorkerId));

        if (filter.specializationIds != null && filter.specializationIds.Any())
            query = query.Where(s => filter.specializationIds.Contains(s.SpecializationId));

        return query;
    }

    public async Task<List<SkillItem>> Get(SkillFilter filter)
    {
        var query = _context.Skills.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "worker" => filter.isDescending
                ? query.OrderByDescending(s => s.Worker == null
                    ? string.Empty
                    : s.Worker.Name)
                : query.OrderBy(s => s.Worker == null
                    ? string.Empty
                    : s.Worker.Name),
            "specialization" => filter.isDescending
                ? query.OrderByDescending(s => s.Specialization == null
                    ? string.Empty
                    : s.Specialization.Name)
                : query.OrderBy(s => s.Specialization == null
                    ? string.Empty
                    : s.Specialization.Name),

            _ => filter.isDescending
                ? query.OrderByDescending(s => s.Id)
                : query.OrderBy(s => s.Id),
        };

        var projection = query.Select(s => new SkillItem(
            s.Id,
            s.Worker == null
                ? string.Empty
                : $"{s.Worker.Name} {s.Worker.Surname}",
            s.Specialization == null
                ? string.Empty
                : s.Specialization.Name));

        return await projection.ToListAsync();
    }

    public async Task<int> GetCount(SkillFilter filter)
    {
        var query = _context.Skills.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<int> Create(Skill skill)
    {
        var skillEntity = new SkillEntity
        {
            WorkerId = skill.WorkerId,
            SpecializationId = skill.SpecializationId,
        };

        await _context.Skills.AddAsync(skillEntity);
        await _context.SaveChangesAsync();

        return skillEntity.Id;
    }

    public async Task<int> Update(int id, SkillUpdateModel model)
    {
        var entity = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new Exception("Schedule note not found");

        if (model.workerId.HasValue) entity.WorkerId = model.workerId.Value;
        if (model.specializationId.HasValue) entity.SpecializationId = model.specializationId.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.Skills
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Exceptions;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class SkillRepository(
    SystemDbContext context,
    IMapper mapper) : ISkillRepository
{
    private static IQueryable<SkillEntity> ApplyFilter(IQueryable<SkillEntity> query, SkillFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
        {
            query = query.Where(s => filter.WorkerIds.Contains(s.WorkerId));
        }

        if (filter.SpecializationIds != null && filter.SpecializationIds.Any())
        {
            query = query.Where(s => filter.SpecializationIds.Contains(s.SpecializationId));
        }

        return query;
    }

    public async Task<List<SkillItem>> Get(SkillFilter filter, CancellationToken ct)
    {
        var query = context.Skills.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "worker" => filter.IsDescending
                ? query.OrderByDescending(s => s.Worker == null
                    ? string.Empty
                    : s.Worker.Name)
                : query.OrderBy(s => s.Worker == null
                    ? string.Empty
                    : s.Worker.Name),

            "specialization" => filter.IsDescending
                ? query.OrderByDescending(s => s.Specialization == null
                    ? string.Empty
                    : s.Specialization.Name)
                : query.OrderBy(s => s.Specialization == null
                    ? string.Empty
                    : s.Specialization.Name),

            _ => filter.IsDescending
                ? query.OrderByDescending(s => s.Id)
                : query.OrderBy(s => s.Id),
        };

        return await query
            .ProjectTo<SkillItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(SkillFilter filter, CancellationToken ct)
    {
        var query = context.Skills.AsNoTracking();

        query = ApplyFilter(query, filter);

        return await query.CountAsync(ct);
    }

    public async Task<int> Create(Skill skill, CancellationToken ct)
    {
        var skillEntity = new SkillEntity
        {
            WorkerId = skill.WorkerId,
            SpecializationId = skill.SpecializationId,
        };

        await context.Skills.AddAsync(skillEntity, ct);
        await context.SaveChangesAsync(ct);

        return skillEntity.Id;
    }

    public async Task<int> Update(int id, SkillUpdateModel model, CancellationToken ct)
    {
        var entity = await context.Skills.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new NotFoundException("Schedule note not found");

        if (model.WorkerId.HasValue)
        {
            entity.WorkerId = model.WorkerId.Value;
        }

        if (model.SpecializationId.HasValue)
        {
            entity.SpecializationId = model.SpecializationId.Value;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        await context.Skills
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class SpecializationRepository(
    SystemDbContext context,
    IMapper mapper) : ISpecializationRepository
{
    public async Task<List<SpecializationItem>> Get(CancellationToken ct)
    {
        return await context.Specializations
            .AsNoTracking()
            .ProjectTo<SpecializationItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(Specialization specialization, CancellationToken ct)
    {
        var specEntity = new SpecializationEntity
        {
            Name = specialization.Name,
        };

        await context.Specializations.AddAsync(specEntity, ct);
        await context.SaveChangesAsync(ct);

        return specEntity.Id;
    }

    public async Task<int> Update(int id, string? name, CancellationToken ct)
    {
        var specialization = await context.Specializations.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new NotFoundException("Specialization not found");

        if (!string.IsNullOrWhiteSpace(name))
        {
            specialization.Name = name;
        }

        await context.SaveChangesAsync(ct);

        return specialization.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        await context.Specializations
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.Specializations
            .AsNoTracking()
            .AnyAsync(s => s.Id == id, ct);
    }

    public  async Task<bool> ExistsByName(string name, CancellationToken ct)
    {
        return await context.Specializations
            .AsNoTracking()
            .AnyAsync(s => s.Name == name, ct);
    }
}


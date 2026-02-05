using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class AbsenceTypeRepository(
    SystemDbContext context,
    IMapper mapper) : IAbsenceTypeRepository
{
    public async Task<List<AbsenceTypeItem>> GetAll(CancellationToken ct)
    {
        return await context.AbsenceTypes
            .AsNoTracking()
            .ProjectTo<AbsenceTypeItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<List<AbsenceTypeItem>> GetByName (string name, CancellationToken ct)
    {
        var query = context.AbsenceTypes
            .Where(a => a.Name == name)
            .AsNoTracking();

        return await context.AbsenceTypes
            .Where(a => a.Name == name)
            .AsNoTracking()
            .ProjectTo<AbsenceTypeItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(AbsenceType absenceType, CancellationToken ct)
    {
        var entity = new AbsenceTypeEntity
        {
            Name = absenceType.Name
        };

        await context.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Update(int id, string name, CancellationToken ct)
    {
        var entity = await context.AbsenceTypes.FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("AbsenceType not found");

        if (!string.IsNullOrEmpty(name))
        {
            entity.Name = name;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        await context.AbsenceTypes
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}

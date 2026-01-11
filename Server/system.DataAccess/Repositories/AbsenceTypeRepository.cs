using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class AbsenceTypeRepository : IAbsenceTypeRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public AbsenceTypeRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AbsenceTypeItem>> GetAll(CancellationToken ct)
    {
        return await _context.AbsenceTypes
            .AsNoTracking()
            .ProjectTo<AbsenceTypeItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<List<AbsenceTypeItem>> GetByName (string name, CancellationToken ct)
    {
        var query = _context.AbsenceTypes
            .Where(a => a.Name == name)
            .AsNoTracking();

        return await _context.AbsenceTypes
            .Where(a => a.Name == name)
            .AsNoTracking()
            .ProjectTo<AbsenceTypeItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(AbsenceType absenceType, CancellationToken ct)
    {
        var entity = new AbsenceTypeEntity
        {
            Name = absenceType.Name
        };

        await _context.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Update(int id, string name, CancellationToken ct)
    {
        var entity = await _context.AbsenceTypes.FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("AbsenceType not found");

        if (entity == null) throw new NotFoundException("AbsencesType not found");

        if (!string.IsNullOrEmpty(name))
            entity.Name = name;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        var entity = await _context.AbsenceTypes
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}

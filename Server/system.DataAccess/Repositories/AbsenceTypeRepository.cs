using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs.AbsenceType;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<AbsenceTypeItem>> GetAll()
    {
        return await _context.AbsenceTypes
            .AsNoTracking()
            .ProjectTo<AbsenceTypeItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<AbsenceTypeItem>> GetByName (string name)
    {
        var query = _context.AbsenceTypes
            .Where(a => a.Name == name)
            .AsNoTracking();

        return await _context.AbsenceTypes
            .Where(a => a.Name == name)
            .AsNoTracking()
            .ProjectTo<AbsenceTypeItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<int> Create(AbsenceType absenceType)
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
        var entity = await _context.AbsenceTypes.FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new Exception("AbsenceType not found");

        if (entity == null) throw new Exception("AbsencesType not found");

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

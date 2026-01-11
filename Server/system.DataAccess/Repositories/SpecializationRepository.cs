using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public SpecializationRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SpecializationItem>> Get(CancellationToken ct)
    {
        return await _context.Specializations
            .AsNoTracking()
            .ProjectTo<SpecializationItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(Specialization specialization, CancellationToken ct)
    {
        var specEntity = new SpecializationEntity
        {
            Name = specialization.Name,
        };

        await _context.Specializations.AddAsync(specEntity, ct);
        await _context.SaveChangesAsync(ct);

        return specEntity.Id;
    }

    public async Task<int> Update(int id, string? name, CancellationToken ct)
    {
        var specialization = await _context.Specializations.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new NotFoundException("Specialization not found");

        if (!string.IsNullOrWhiteSpace(name))
            specialization.Name = name;

        await _context.SaveChangesAsync(ct);

        return specialization.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        var specialization = await _context.Specializations
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.Specializations
            .AsNoTracking()
            .AnyAsync(s => s.Id == id, ct);
    }

    public  async Task<bool> ExistsByName(string name, CancellationToken ct)
    {
        return await _context.Specializations
            .AsNoTracking()
            .AnyAsync(s => s.Name == name, ct);
    }
}


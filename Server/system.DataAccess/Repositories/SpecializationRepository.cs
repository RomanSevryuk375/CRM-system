using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly SystemDbContext _context;

    public SpecializationRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<SpecializationItem>> Get()
    {
        var query = _context.Specializations.AsNoTracking();

        var projection = query
            .Select(s => new SpecializationItem(
                s.Id,
                s.Name));

        return await projection.ToListAsync();
    }

    public async Task<int> Create(Specialization specialization)
    {
        var specEntity = new SpecializationEntity
        {
            Name = specialization.Name,
        };

        await _context.Specializations.AddAsync(specEntity);
        await _context.SaveChangesAsync();

        return specEntity.Id;
    }

    public async Task<int> Update(int id, string? name)
    {
        var specialization = await _context.Specializations.FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new Exception("Specialization not found");

        if (!string.IsNullOrWhiteSpace(name))
            specialization.Name = name;

        await _context.SaveChangesAsync();

        return specialization.Id;
    }

    public async Task<int> Delete(int id)
    {
        var specialization = await _context.Specializations
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Specializations
            .AsNoTracking()
            .AnyAsync(s => s.Id == id);
    }

    public  async Task<bool> ExistsByName(string name)
    {
        return await _context.Specializations
            .AsNoTracking()
            .AnyAsync(s => s.Name == name);
    }
}


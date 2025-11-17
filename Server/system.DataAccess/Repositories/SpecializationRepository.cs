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

    public async Task<List<Specialization>> Get()
    {
        var specializationEntities = await _context.Specializations
            .AsNoTracking()
            .ToListAsync();

        var specializations = specializationEntities
            .Select(s => Specialization.Create(
                s.Id,
                s.Name
                ).specialization)
            .ToList();

        return specializations;
    }

    public async Task<int> GetCount()
    {
        return await _context.Specializations.CountAsync();
    }

    public async Task<int> Create(Specialization specialization)
    {
        var (_, error) = Specialization.Create(
            0,
            specialization.Name);
        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Specialization: {error}");

        var specializationEntities = new SpecializationEntity
        {
            Name = specialization.Name,
        };

        await _context.Specializations.AddAsync(specializationEntities);
        await _context.SaveChangesAsync();

        return specializationEntities.Id;
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
}


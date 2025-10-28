using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRMSystem.DataAccess.Repositories;

public class TaxRepositoty
{
    private readonly SystemDbContext _context;

    public TaxRepositoty(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tax>> Get()
    {
        var taxEntities = await _context.Taxes
            .AsNoTracking()
            .ToListAsync();

        var taxes = taxEntities.Select(
            t => Tax.Create(
                t.Id,
                t.Name,
                t.Rate,
                t.Type).tax)
            .ToList();

        return taxes;        
    }

    public async Task<int> Create(Tax tax)
    {
        var (_, error) = Tax.Create(
            0,
            tax.Name,
            tax.Rate, 
            tax.Type);  
        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Tax: {error}");

        var taxEntitie = new TaxEntity
        {
            Name = tax.Name,
            Rate = tax.Rate,
            Type = tax.Type,
        };

        await _context.Taxes.AddAsync(taxEntitie);
        await _context.SaveChangesAsync();

        return taxEntitie.Id;
    }

    public async Task<int> Update (int id, string name, decimal rate, string type)
    {
        var tax = _context.Taxes.FirstOrDefault(t => t.Id == id)
            ?? throw new ArgumentException("Tax not found");

        if (!string.IsNullOrWhiteSpace(name))
            tax.Name = name;
        if (!string.IsNullOrWhiteSpace(type))
            tax.Type = type;

        await _context.SaveChangesAsync();

        return tax.Id;
    }

    public async Task<int> Delete (int id)
    {
        var tax = await _context.Taxes
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

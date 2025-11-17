using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly SystemDbContext _context;

    public SupplierRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<Supplier>> Get()
    {
        var supplierEntitie = await _context.Suppliers
            .AsNoTracking()
            .ToListAsync();

        var suppliers = supplierEntitie
            .Select(su => Supplier.Create(
                su.Id,
                su.Name,
                su.Contacts
                ).supplier)
            .ToList();

        return suppliers;
    }

    public async Task<List<Supplier>> GetPaged(int page, int limit)
    {
        var supplierEntitie = await _context.Suppliers
                    .AsNoTracking()
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

        var suppliers = supplierEntitie
            .Select(su => Supplier.Create(
                su.Id,
                su.Name,
                su.Contacts
                ).supplier)
            .ToList();

        return suppliers;
    }
    
    public async Task<int> GetCount()
    {
        return await _context.Suppliers.CountAsync();
    }

    public async Task<int> Create(Supplier supplier)
    {
        var (_, error) = Supplier.Create(
            0,
            supplier.Name,
            supplier.Contacts);
        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Supplier: {error}");

        var supplierEntity = new SupplierEntity
        {
            Name = supplier.Name,
            Contacts = supplier.Contacts
        };

        await _context.Suppliers.AddAsync(supplierEntity);
        await _context.SaveChangesAsync();

        return supplierEntity.Id;
    }

    public async Task<int> Update(int id, string? name, string? contacts)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(su => su.Id == id)
            ?? throw new ArgumentException("Supplier not found");

        if (!string.IsNullOrWhiteSpace(name))
            supplier.Name = name;
        if (!string.IsNullOrWhiteSpace(contacts))
            supplier.Contacts = contacts;

        await _context.SaveChangesAsync();

        return supplier.Id;
    }

    public async Task<int> Delete(int id)
    {
        var supplier = await _context.Suppliers
            .Where(su => su.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

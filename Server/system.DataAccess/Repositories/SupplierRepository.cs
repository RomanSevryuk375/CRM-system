using CRMSystem.Core.DTOs.Supplier;
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

    public async Task<List<SupplierItem>> Get()
    {
        var query = _context.Suppliers.AsNoTracking();

        var suppliers = query.Select(su => new SupplierItem(
                su.Id,
                su.Name,
                su.Contacts
                ));

        return await suppliers.ToListAsync();
    }

    public async Task<int> Create(Supplier supplier)
    {
        var supplierEntity = new SupplierEntity
        {
            Name = supplier.Name,
            Contacts = supplier.Contacts
        };

        await _context.Suppliers.AddAsync(supplierEntity);
        await _context.SaveChangesAsync();

        return supplierEntity.Id;
    }

    public async Task<int> Update(int id, SupplierUpdateModel model)
    {
        var entity = await _context.Suppliers.FirstOrDefaultAsync(su => su.Id == id)
            ?? throw new ArgumentException("Supplier not found");

        if (!string.IsNullOrWhiteSpace(model.name)) entity.Name = model.name;
        if (!string.IsNullOrWhiteSpace(model.contacts)) entity.Contacts = model.contacts;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var supplier = await _context.Suppliers
            .Where(su => su.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}

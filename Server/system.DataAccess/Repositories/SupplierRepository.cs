using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs.Supplier;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public SupplierRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SupplierItem>> Get()
    {
        return await _context.Suppliers
            .AsNoTracking()
            .ProjectTo<SupplierItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
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

        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Contacts)) entity.Contacts = model.Contacts;

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

    public async Task<bool> Exists(int id)
    {
        return await _context.Suppliers
            .AsNoTracking()
            .AnyAsync(s => s.Id == id);
    }

    public async Task<bool> ExistsByName(string name)
    {
        return await _context.Suppliers
            .AsNoTracking()
            .AnyAsync(s => s.Name == name);
    }
}

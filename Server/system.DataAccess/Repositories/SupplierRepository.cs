using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Supplier;
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

    public async Task<List<SupplierItem>> Get(CancellationToken ct)
    {
        return await _context.Suppliers
            .AsNoTracking()
            .ProjectTo<SupplierItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(Supplier supplier, CancellationToken ct)
    {
        var supplierEntity = new SupplierEntity
        {
            Name = supplier.Name,
            Contacts = supplier.Contacts
        };

        await _context.Suppliers.AddAsync(supplierEntity, ct);
        await _context.SaveChangesAsync(ct);

        return supplierEntity.Id;
    }

    public async Task<int> Update(int id, SupplierUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Suppliers.FirstOrDefaultAsync(su => su.Id == id, ct)
            ?? throw new ArgumentException("Supplier not found");

        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Contacts)) entity.Contacts = model.Contacts;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        var supplier = await _context.Suppliers
            .Where(su => su.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.Suppliers
            .AsNoTracking()
            .AnyAsync(s => s.Id == id, ct);
    }

    public async Task<bool> ExistsByName(string name, CancellationToken ct)
    {
        return await _context.Suppliers
            .AsNoTracking()
            .AnyAsync(s => s.Name == name, ct);
    }
}

using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<List<Supplier>> GetPagedSupplier(int page, int limit)
    {
        return await _supplierRepository.GetPaged(page, limit);
    }

    public async Task<int> GetCountSupplier()
    {
        return await _supplierRepository.GetCount();
    }

    public async Task<int> CreateSupplier(Supplier supplier)
    {
        return await _supplierRepository.Create(supplier);
    }

    public async Task<int> UpdateSupplier(int id, string? name, string? contacts)
    {
        return await _supplierRepository.Update(id, name, contacts);
    }

    public async Task<int> DeleteSupplier(int id)
    {
        return await _supplierRepository.Delete(id);
    }
}

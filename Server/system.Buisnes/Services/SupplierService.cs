using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Supplier;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace CRMSystem.Buisnes.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(
        ISupplierRepository supplierRepository,
        ILogger<SupplierService> logger)
    {
        _supplierRepository = supplierRepository;
        _logger = logger;
    }

    public async Task<List<SupplierItem>> GetSuppliers()
    {
        _logger.LogInformation("Getting suppliers start");

        var suppliers = await _supplierRepository.Get();

        _logger.LogInformation("Getting suppliers success");

        return suppliers;
    }

    public async Task<int> CreateSupplier(Supplier supplier)
    {
        _logger.LogInformation("Creating supplier start");

        if (await _supplierRepository.ExistsByName(supplier.Name))
        {
            _logger.LogError("Supplier is exist with this name{supplierName}", supplier.Name);
            throw new ConflictException($"Supplier is exist with this name{supplier.Name}");
        }

        var Id = await _supplierRepository.Create(supplier);

        _logger.LogInformation("Creating supplier success");

        return Id;
    }

    public async Task<int> UpdateSupplier(int id, SupplierUpdateModel model)
    {
        _logger.LogInformation("Updating supplier start");

        var Id = await _supplierRepository.Update(id, model);

        _logger.LogInformation("Updating supplier success");

        return Id;
    }

    public async Task<int> DeleteSupplier(int id)
    {
        _logger.LogInformation("Deleting supplier start");

        var Id = await _supplierRepository.Delete(id);

        _logger.LogInformation("Deleting supplier success");

        return Id;
    }
}

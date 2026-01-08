using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

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

    public async Task<List<SupplierItem>> GetSuppliers(CancellationToken ct)
    {
        _logger.LogInformation("Getting suppliers start");

        var suppliers = await _supplierRepository.Get(ct);

        _logger.LogInformation("Getting suppliers success");

        return suppliers;
    }

    public async Task<int> CreateSupplier(Supplier supplier, CancellationToken ct)
    {
        _logger.LogInformation("Creating supplier start");

        if (await _supplierRepository.ExistsByName(supplier.Name, ct))
        {
            _logger.LogError("Supplier is exist with this name{supplierName}", supplier.Name);
            throw new ConflictException($"Supplier is exist with this name{supplier.Name}");
        }

        var Id = await _supplierRepository.Create(supplier, ct);

        _logger.LogInformation("Creating supplier success");

        return Id;
    }

    public async Task<int> UpdateSupplier(int id, SupplierUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating supplier start");

        var Id = await _supplierRepository.Update(id, model, ct);

        _logger.LogInformation("Updating supplier success");

        return Id;
    }

    public async Task<int> DeleteSupplier(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting supplier start");

        var Id = await _supplierRepository.Delete(id, ct);

        _logger.LogInformation("Deleting supplier success");

        return Id;
    }
}

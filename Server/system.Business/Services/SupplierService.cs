using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class SupplierService(
    ISupplierRepository supplierRepository,
    ILogger<SupplierService> logger) : ISupplierService
{
    public async Task<List<SupplierItem>> GetSuppliers(CancellationToken ct)
    {
        logger.LogInformation("Getting suppliers start");

        var suppliers = await supplierRepository.Get(ct);

        logger.LogInformation("Getting suppliers success");

        return suppliers;
    }

    public async Task<int> CreateSupplier(SupplierCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating supplier start");

        if (await supplierRepository.ExistsByName(createModel.Name, ct))
        {
            logger.LogError("Supplier is exist with this name{supplierName}", createModel.Name);
            throw new ConflictException($"Supplier is exist with this name{createModel.Name}");
        }

        var (supplier, errors) = Supplier.Create(
            0,
            createModel.Name,
            createModel.Contacts);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }
        
        var id = await supplierRepository.Create(supplier!, ct);

        logger.LogInformation("Creating supplier success");

        return id;
    }

    public async Task<int> UpdateSupplier(int id, SupplierUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating supplier start");

        var supplierId = await supplierRepository.Update(id, model, ct);

        logger.LogInformation("Updating supplier success");

        return supplierId;
    }

    public async Task<int> DeleteSupplier(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting supplier start");

        var supplierId = await supplierRepository.Delete(id, ct);

        logger.LogInformation("Deleting supplier success");

        return supplierId;
    }
}

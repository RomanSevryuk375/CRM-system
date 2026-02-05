using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class TaxService(
    ITaxRepository taxRepository,
    ITaxTypeRepository taxTypeRepository,
    ILogger<TaxService> logger) : ITaxService
{
    public async Task<List<TaxItem>> GetTaxes(TaxFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting tax start");

        var taxes = await taxRepository.Get(filter, ct);

        logger.LogInformation("Getting tax success");

        return taxes;
    }

    public async Task<int> CreateTax(Tax tax, CancellationToken ct)
    {
        logger.LogInformation("Creating tax start");

        if (!await taxTypeRepository.Exists((int)tax.TypeId, ct))
        {
            logger.LogError("Tax{taxId} not found", (int)tax.TypeId);
            throw new NotFoundException($"Tax {(int)tax.TypeId} not found");
        }

        var Id = await taxRepository.Create(tax, ct);

        logger.LogInformation("Creating tax success");

        return Id;
    }

    public async Task<int> UpdateTax(int id, TaxUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating tax start");

        var Id = await taxRepository.Update(id, model, ct);

        logger.LogInformation("Updating tax success");

        return Id;
    }

    public async Task<int> DeleteTax(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting tax start");

        var Id = await taxRepository.Delete(id, ct);

        logger.LogInformation("Deleting tax success");

        return Id;
    }
}

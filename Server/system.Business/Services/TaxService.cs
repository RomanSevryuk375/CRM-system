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
    public async Task<TaxItem> GetTaxById(int id, CancellationToken ct)
    {
        return await taxRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Tax {id} not found");
    }
    
    public async Task<List<TaxItem>> GetTaxes(TaxFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting tax start");

        var taxes = await taxRepository.Get(filter, ct);

        logger.LogInformation("Getting tax success");

        return taxes;
    }

    public async Task<int> CreateTax(TaxCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating tax start");

        if (!await taxTypeRepository.Exists((int)createModel.TypeId, ct))
        {
            logger.LogError("Tax{taxId} not found", (int)createModel.TypeId);
            throw new NotFoundException($"Tax {(int)createModel.TypeId} not found");
        }
        
        var (tax, errors) = Tax.Create(
            0,
            createModel.Name,
            createModel.Rate,
            createModel.TypeId);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await taxRepository.Create(tax!, ct);

        logger.LogInformation("Creating tax success");

        return id;
    }

    public async Task<int> UpdateTax(int id, TaxUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating tax start");

        var taxId = await taxRepository.Update(id, model, ct);

        logger.LogInformation("Updating tax success");

        return taxId;
    }

    public async Task<int> DeleteTax(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting tax start");

        var taxId = await taxRepository.Delete(id, ct);

        logger.LogInformation("Deleting tax success");

        return taxId;
    }
}

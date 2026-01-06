using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class TaxService : ITaxService
{
    private readonly ITaxRepository _taxRepository;
    private readonly ITaxTypeRepository _taxTypeRepository;
    private readonly ILogger<TaxService> _logger;

    public TaxService(
        ITaxRepository taxRepository,
        ITaxTypeRepository taxTypeRepository,
        ILogger<TaxService> logger)
    {
        _taxRepository = taxRepository;
        _taxTypeRepository = taxTypeRepository;
        _logger = logger;
    }

    public async Task<List<TaxItem>> GetTaxes(TaxFilter filter)
    {
        _logger.LogInformation("Getting tax start");

        var taxes = await _taxRepository.Get(filter);

        _logger.LogInformation("Getting tax success");

        return taxes;
    }

    public async Task<int> CreateTax(Tax tax)
    {
        _logger.LogInformation("Creating tax start");

        if (!await _taxTypeRepository.Exists((int)tax.TypeId))
        {
            _logger.LogError("Tax{taxId} not found", (int)tax.TypeId);
            throw new NotFoundException($"Tax {(int)tax.TypeId} not found");
        }

        var Id = await _taxRepository.Create(tax);

        _logger.LogInformation("Creating tax success");

        return Id;
    }

    public async Task<int> UpdateTax(int id, TaxUpdateModel model)
    {
        _logger.LogInformation("Updating tax start");

        var Id = await _taxRepository.Update(id, model);

        _logger.LogInformation("Updating tax success");

        return Id;
    }

    public async Task<int> DeleteTax(int id)
    {
        _logger.LogInformation("Deleting tax start");

        var Id = await _taxRepository.Delete(id);

        _logger.LogInformation("Deleting tax success");

        return Id;
    }
}

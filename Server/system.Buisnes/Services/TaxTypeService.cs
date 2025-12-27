using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class TaxTypeService : ITaxTypeService
{
    private readonly ITaxTypeRepository _taxTypeRepository;
    private readonly ILogger<TaxTypeService> _logger;

    public TaxTypeService(
        ITaxTypeRepository taxTypeRepository,
        ILogger<TaxTypeService> logger)
    {
        _taxTypeRepository = taxTypeRepository;
        _logger = logger;
    }

    public async Task<List<TaxTypeItem>> GetTaxTypes()
    {
        _logger.LogInformation("Getting tax type start");

        var taxTypes = await _taxTypeRepository.Get();

        _logger.LogInformation("Getting tax types success");

        return taxTypes;
    }
}

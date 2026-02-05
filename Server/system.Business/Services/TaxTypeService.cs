using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class TaxTypeService(
    ITaxTypeRepository taxTypeRepository,
    ILogger<TaxTypeService> logger) : ITaxTypeService
{
    public async Task<List<TaxTypeItem>> GetTaxTypes(CancellationToken ct)
    {
        logger.LogInformation("Getting tax type start");

        var taxTypes = await taxTypeRepository.Get(ct);

        logger.LogInformation("Getting tax types success");

        return taxTypes;
    }
}

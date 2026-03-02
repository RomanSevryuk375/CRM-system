using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.TaxType;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class TaxTypeService(
    ITaxTypeRepository taxTypeRepository,
    ILogger<TaxTypeService> logger) : ITaxTypeService
{
    public async Task<TaxTypeItem> GetTaxTypeById(int id, CancellationToken ct)
    {
        return await taxTypeRepository.GetById(id, ct)
               ?? throw new NotFoundException($"TaxType {id} not found");
    }
    
    public async Task<List<TaxTypeItem>> GetTaxTypes(CancellationToken ct)
    {
        logger.LogInformation("Getting tax type start");

        var taxTypes = await taxTypeRepository.Get(ct);

        logger.LogInformation("Getting tax types success");

        return taxTypes;
    }
}

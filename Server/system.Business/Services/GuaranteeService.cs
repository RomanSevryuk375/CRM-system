using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class GuaranteeService(
    IGuaranteeRepository guaranteeRepository,
    IOrderRepository orderRepository,
    ILogger<GuaranteeService> logger) : IGuaranteeService
{
    public async Task<GuaranteeItem> GetGuaranteeById(long id, CancellationToken ct)
    {
        return await guaranteeRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Guarantee {id} not found");
    }
    
    public async Task<List<GuaranteeItem>> GetPagedGuarantees(GuaranteeFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting guarantees start"); 

        var guarantee = await guaranteeRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting guarantees success"); 

        return guarantee;
    }

    public async Task<int> GetCountGuarantees(GuaranteeFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count guarantees start"); 

        var count = await guaranteeRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count guarantees success"); 

        return count;
    }

    public async Task<long> CreateGuarantee(GuaranteeCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating guarantee for order {OrderId} start", createModel.OrderId);

        if (!await orderRepository.Exists(createModel.OrderId, ct))
        {
            logger.LogError("Order{OrderId} not found", createModel.OrderId);
            throw new NotFoundException($"Order{createModel.OrderId} not found");
        }
        
        var (guarantee, errors) = Guarantee.Create(
            0,
            createModel.OrderId,
            createModel.DateStart,
            createModel.DateEnd, 
            createModel.Description, 
            createModel.Terms);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await guaranteeRepository.Create(guarantee!, ct);

        logger.LogInformation("Creating guarantee for order {OrderId} success with ID {GuaranteeId}",
            createModel.OrderId, id);

        return id;
    }

    public async Task<long> UpdateGuarantee(long id, GuaranteeUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating guarantee {Id} start", id);

        var guaranteeId = await guaranteeRepository.Update(id, model, ct);

        logger.LogInformation("Updating guarantee {Id} success", id);

        return guaranteeId;
    }

    public async Task<long> DeleteGuarantee(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting guarantee {Id} start", id);

        var guaranteeId = await guaranteeRepository.Delete(id, ct);

        logger.LogInformation("Deleting guarantee {Id} success", id);

        return guaranteeId;
    }
}

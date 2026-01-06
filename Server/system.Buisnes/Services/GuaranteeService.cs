using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class GuaranteeService : IGuaranteeService
{
    private readonly IGuaranteeRepository _guaranteeRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GuaranteeService> _logger;

    public GuaranteeService(
        IGuaranteeRepository guaranteeRepository,
        IOrderRepository orderRepository,
        ILogger<GuaranteeService> logger)
    {
        _guaranteeRepository = guaranteeRepository;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<List<GuaranteeItem>> GetPagedGuarantees(GuaranteeFilter filter)
    {
        _logger.LogInformation("Getting guarantees start"); 

        var guarantee = await _guaranteeRepository.GetPaged(filter);

        _logger.LogInformation("Getting guarantees success"); 

        return guarantee;
    }

    public async Task<int> GetCountGuarantees(GuaranteeFilter filter)
    {
        _logger.LogInformation("Getting count guarantees start"); 

        var count = await _guaranteeRepository.GetCount(filter);

        _logger.LogInformation("Getting count guarantees success"); 

        return count;
    }

    public async Task<long> CreateGuarantee(Guarantee guarantee)
    {
        _logger.LogInformation("Creating guarantee for order {OrderId} start", guarantee.OrderId);

        if (!await _orderRepository.Exists(guarantee.OrderId))
        {
            _logger.LogError("Order{OrderId} not found", guarantee.OrderId);
            throw new NotFoundException($"Order{guarantee.OrderId} not found");
        }

        var Id = await _guaranteeRepository.Create(guarantee);

        _logger.LogInformation("Creating guarantee for order {OrderId} success with ID {GuaranteeId}", guarantee.OrderId, Id);

        return Id;
    }

    public async Task<long> UpdateGuarantee(long id, GuaranteeUpdateModel model)
    {
        _logger.LogInformation("Updating guarantee {Id} start", id);

        var Id = await _guaranteeRepository.Update(id, model);

        _logger.LogInformation("Updating guarantee {Id} success", id);

        return Id;
    }

    public async Task<long> DeleteGuarantee(long id)
    {
        _logger.LogInformation("Deleting guarantee {Id} start", id);

        var Id = await _guaranteeRepository.Delete(id);

        _logger.LogInformation("Deleting guarantee {Id} success", id);

        return Id;
    }
}

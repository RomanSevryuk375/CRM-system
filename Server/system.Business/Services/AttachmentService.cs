using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly ILogger<AttachmentService> _logger;

    public AttachmentService(
        IAttachmentRepository attachmentRepository,
        IOrderRepository orderRepository,
        IWorkerRepository workerRepository,
        ILogger<AttachmentService> logger)
    {
        _attachmentRepository = attachmentRepository;
        _orderRepository = orderRepository;
        _workerRepository = workerRepository;
        _logger = logger;
    }

    public async Task<List<AttachmentItem>> GetPagedAttachments(AttachmentFilter filter)
    {
        _logger.LogInformation("Getting attachments start");

        var attachment = await _attachmentRepository.GetPaged(filter);

        _logger.LogInformation("Getting attachments success");

        return attachment;
    }

    public async Task<int> GetCountAttachment(AttachmentFilter filter)
    {
        _logger.LogInformation("Getting count attachments start");

        var count = await _attachmentRepository.GetCount(filter);

        _logger.LogInformation("Getting count attachments success");

        return count;
    }

    public async Task<long> CreateAttachment(Attachment attachment)
    {
        _logger.LogInformation("Creating attachments start");

        if (!await _orderRepository.Exists(attachment.OrderId))
        {
            _logger.LogInformation("Order{OrderId} not found", attachment.OrderId);
            throw new NotFoundException($"Order{attachment.OrderId} not found");
        }

        if (!await _workerRepository.Exists(attachment.WorkerId))
        {
            _logger.LogInformation("Worker{WorkerId} not found", attachment.WorkerId);
            throw new NotFoundException($"Worker{attachment.WorkerId} not found");
        }

        var id = await _attachmentRepository.Create(attachment);

        _logger.LogInformation("Creating attachments success");

        return id;
    }

    public async Task<long> UpdateAttachment(long id, string? description)
    {
        _logger.LogInformation("Updating attachments start");

        var Id = await _attachmentRepository.Update(id, description);

        _logger.LogInformation("Updating attachments success");

        return Id;
    }

    public async Task<long> DeletingAttachment(long id)
    {
        _logger.LogInformation("Deleting attachments start");

        var Id = await _attachmentRepository.Delete(id);

        _logger.LogInformation("Deleting attachments success");

        return Id;
    }
}

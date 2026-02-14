using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class AttachmentService(
    IAttachmentRepository attachmentRepository,
    IOrderRepository orderRepository,
    IWorkerRepository workerRepository,
    ILogger<AttachmentService> logger) : IAttachmentService
{
    public async Task<List<AttachmentItem>> GetPagedAttachments(AttachmentFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting attachments start");

        var attachment = await attachmentRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting attachments success");

        return attachment;
    }

    public async Task<int> GetCountAttachment(AttachmentFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count attachments start");

        var count = await attachmentRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count attachments success");

        return count;
    }

    public async Task<long> CreateAttachment(Attachment attachment, CancellationToken ct)
    {
        logger.LogInformation("Creating attachments start");

        if (!await orderRepository.Exists(attachment.OrderId, ct))
        {
            logger.LogInformation("Order{OrderId} not found", attachment.OrderId);
            throw new NotFoundException($"Order{attachment.OrderId} not found");
        }

        if (!await workerRepository.Exists(attachment.WorkerId, ct))
        {
            logger.LogInformation("Worker{WorkerId} not found", attachment.WorkerId);
            throw new NotFoundException($"Worker{attachment.WorkerId} not found");
        }

        var id = await attachmentRepository.Create(attachment, ct);

        logger.LogInformation("Creating attachments success");

        return id;
    }

    public async Task<long> UpdateAttachment(long id, string? description, CancellationToken ct)
    {
        logger.LogInformation("Updating attachments start");

        var Id = await attachmentRepository.Update(id, description, ct);

        logger.LogInformation("Updating attachments success");

        return Id;
    }

    public async Task<long> DeletingAttachment(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting attachments start");

        var Id = await attachmentRepository.Delete(id, ct);

        logger.LogInformation("Deleting attachments success");

        return Id;
    }
}

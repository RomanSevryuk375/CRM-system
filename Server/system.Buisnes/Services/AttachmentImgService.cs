using CRMSystem.Core.DTOs.Attachment;
using CRMSystem.Core.DTOs.AttachmentImg;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class AttachmentImgService
{
    private readonly IAttachmentImgRepository _attachmentImgRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly ILogger<AttachmentImgService> _logger;

    public AttachmentImgService(
        IAttachmentImgRepository attachmentImgRepository,
        IAttachmentRepository attachmentRepository,
        ILogger<AttachmentImgService> logger)
    {
        _attachmentImgRepository = attachmentImgRepository;
        _attachmentRepository = attachmentRepository;
        _logger = logger;
    }

    public async Task<List<AttachmentImgItem>> GetPagedAttachmentImg(AttachmentImgFilter filter)
    {
        _logger.LogInformation("AttachmentImg getting start");

        var attachmentImg = await _attachmentImgRepository.GetPaged(filter);

        _logger.LogInformation("AttachmentImg getting success");

        return attachmentImg;
    }

    public async Task<int> GetCountAttachmentImg (AttachmentImgFilter filter)
    {
        _logger.LogInformation("AttachmentImg getting count start");

        var count = await _attachmentImgRepository.GetCount(filter);

        _logger.LogInformation("AttachmentImg getting count success");

        return count;
    }

    public async Task<long> CreateAttachmentImg (AttachmentImg attachmentImg)
    {
        _logger.LogInformation("AttachmentImg creating strart");

        if (!await _attachmentRepository.Exists(attachmentImg.AttachmentId))
        {
            _logger.LogInformation("Attachment{AttachmentId} not found" ,attachmentImg.AttachmentId);
            throw new NotFoundException($"Attachment {attachmentImg.AttachmentId} not found");
        }

        var attachmentImgRes = await _attachmentImgRepository.Create(attachmentImg);

        _logger.LogInformation("AttachmentImg creating success");

        return attachmentImgRes;
    }

    public async Task<long> UpdateAttaachmentImg(long id, string? filePath, string? description)
    {
        _logger.LogInformation("AttachmentImg updating strart");

        var attachmentImg = await _attachmentImgRepository.Update(id, filePath, description);

        _logger.LogInformation("AttachmentImg updating success");

        return attachmentImg;
    }

    public async Task<long> DeleteAttachmentImg(long id)
    {
        _logger.LogInformation("AttachmentImg deleting strart");

        var attachmentImg = await _attachmentImgRepository.Delete(id);

        _logger.LogInformation("AttachmentImg deleting strart");

        return attachmentImg;
    }
}

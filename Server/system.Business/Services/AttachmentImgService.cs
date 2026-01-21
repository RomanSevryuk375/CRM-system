// Ignore Spelling: Img

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class AttachmentImgService : IAttachmentImgService
{
    private readonly IAttachmentImgRepository _attachmentImgRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly ILogger<AttachmentImgService> _logger;
    private readonly IFileService _fileService;

    public AttachmentImgService(
        IAttachmentImgRepository attachmentImgRepository,
        IAttachmentRepository attachmentRepository,
        ILogger<AttachmentImgService> logger,
        IFileService fileService)
    {
        _attachmentImgRepository = attachmentImgRepository;
        _attachmentRepository = attachmentRepository;
        _logger = logger;
        _fileService = fileService;
    }

    public async Task<List<AttachmentImgItem>> GetPagedAttachmentImg(AttachmentImgFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("AttachmentImg getting start");

        var attachmentImg = await _attachmentImgRepository.GetPaged(filter, ct);

        _logger.LogInformation("AttachmentImg getting success");

        return attachmentImg;
    }

    public async Task<(Stream FileStream, string ContentType)> GetImageStream(long id, CancellationToken ct)
    {
        var img = await _attachmentImgRepository.GetById(id, ct) 
            ?? throw new NotFoundException($"Image {id} not found");

        var stream = await _fileService.GetFile(img.FilePath, ct);

        string contentType = "application/octet-stream";

        return (stream, contentType);
    }

    public async Task<int> GetCountAttachmentImg(AttachmentImgFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("AttachmentImg getting count start");

        var count = await _attachmentImgRepository.GetCount(filter, ct);

        _logger.LogInformation("AttachmentImg getting count success");

        return count;
    }

    public async Task<long> CreateAttachmentImg(long attachmentId, FileItem file, string? description, CancellationToken ct)
    {
        _logger.LogInformation("AttachmentImg creating start");

        if (!await _attachmentRepository.Exists(attachmentId, ct))
        {
            _logger.LogInformation("Attachment{AttachmentId} not found", attachmentId);
            throw new NotFoundException($"Attachment {attachmentId} not found");
        }

        string path;
        try
        {
            path = await _fileService.UploadFile(file.Content, file.FileName, file.ContentType, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload file to storage");
            throw new Exception("File storage error");
        }

        var (attachmentImg, errors) = AttachmentImg.Create(
            0,
            attachmentId,
            path,
            description);

        if (errors is not null && errors.Any() || attachmentImg == null)
        {
            await _fileService.DeleteFile(path, ct);

            var errorMsg = string.Join(", ", errors!);
            _logger.LogError("attachmentImg validation failed: {Errors}", errorMsg);
            throw new ConflictException($"Validation failed: {errorMsg}");
        }

        var attachmentImgRes = await _attachmentImgRepository.Create(attachmentImg, ct);

        _logger.LogInformation("AttachmentImg creating success");

        return attachmentImgRes;
    }

    public async Task<long> UpdateAttachmentImg(long id, string? filePath, string? description, CancellationToken ct)
    {
        _logger.LogInformation("AttachmentImg updating start");

        var attachmentImg = await _attachmentImgRepository.Update(id, filePath, description, ct);

        _logger.LogInformation("AttachmentImg updating success");

        return attachmentImg;
    }

    public async Task<long> DeleteAttachmentImg(long id, CancellationToken ct)
    {
        _logger.LogInformation("AttachmentImg deleting start");

        var img = await _attachmentImgRepository.GetById(id, ct);

        if (img is null)
        {
            _logger.LogInformation("AttachmentImg{AcceptanceImgId} not found", id);
            throw new NotFoundException($"AttachmentImg {id} not found");
        }

        await _fileService.DeleteFile(img.FilePath, ct);

        var attachmentImg = await _attachmentImgRepository.Delete(id, ct);

        _logger.LogInformation("AttachmentImg deleting start");

        return attachmentImg;
    }
}

using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisness.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.DTOs.AttachmentImg;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<AttachmentImgItem>> GetPagedAttachmentImg(AttachmentImgFilter filter)
    {
        _logger.LogInformation("AttachmentImg getting start");

        var attachmentImg = await _attachmentImgRepository.GetPaged(filter);

        _logger.LogInformation("AttachmentImg getting success");

        return attachmentImg;
    }

    public async Task<(Stream FileStream, string ContentType)> GetImageStream(long id)
    {
        var img = await _attachmentImgRepository.GetById(id);
        if (img == null)
            throw new NotFoundException($"Image {id} not found");

        var stream = await _fileService.GetFile(img.filePath);

        string contentType = "application/octet-stream";

        return (stream, contentType);
    }

    public async Task<int> GetCountAttachmentImg(AttachmentImgFilter filter)
    {
        _logger.LogInformation("AttachmentImg getting count start");

        var count = await _attachmentImgRepository.GetCount(filter);

        _logger.LogInformation("AttachmentImg getting count success");

        return count;
    }

    public async Task<long> CreateAttachmentImg(long attachmentId, FileItem file, string? description)
    {
        _logger.LogInformation("AttachmentImg creating strart");

        if (!await _attachmentRepository.Exists(attachmentId))
        {
            _logger.LogInformation("Attachment{AttachmentId} not found", attachmentId);
            throw new NotFoundException($"Attachment {attachmentId} not found");
        }

        string path;
        try
        {
            path = await _fileService.UploadFile(file.Content, file.FileName, file.ContentType);
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

        if ((errors is not null && errors.Any()) || attachmentImg == null)
        {
            await _fileService.DeleteFile(path);

            var errorMsg = string.Join(", ", errors!);
            _logger.LogError("attachmentImg validation failed: {Errors}", errorMsg);
            throw new ConflictException($"Validation failed: {errorMsg}");
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

        var img = await _attachmentImgRepository.GetById(id);

        if (img is null)
        {
            _logger.LogInformation("AttachmentImg{AcceptanceImgId} not found", id);
            throw new NotFoundException($"AttachmentImg {id} not found");
        }

        await _fileService.DeleteFile(img.filePath);

        var attachmentImg = await _attachmentImgRepository.Delete(id);

        _logger.LogInformation("AttachmentImg deleting strart");

        return attachmentImg;
    }
}

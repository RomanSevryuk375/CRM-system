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

public class AttachmentImgService(
    IAttachmentImgRepository attachmentImgRepository,
    IAttachmentRepository attachmentRepository,
    ILogger<AttachmentImgService> logger,
    IFileService fileService) : IAttachmentImgService
{
    public async Task<List<AttachmentImgItem>> GetPagedAttachmentImg(
        AttachmentImgFilter filter, CancellationToken ct)
    {
        logger.LogInformation("AttachmentImg getting start");

        var attachmentImg = await attachmentImgRepository.GetPaged(filter, ct);

        logger.LogInformation("AttachmentImg getting success");

        return attachmentImg;
    }

    public async Task<(Stream FileStream, string ContentType)> GetImageStream(long id, CancellationToken ct)
    {
        var img = await attachmentImgRepository.GetById(id, ct) 
            ?? throw new NotFoundException($"Image {id} not found");

        var stream = await fileService.GetFile(img.FilePath, ct);

        const string contentType = "application/octet-stream";

        return (stream, contentType);
    }

    public async Task<int> GetCountAttachmentImg(AttachmentImgFilter filter, CancellationToken ct)
    {
        logger.LogInformation("AttachmentImg getting count start");

        var count = await attachmentImgRepository.GetCount(filter, ct);

        logger.LogInformation("AttachmentImg getting count success");

        return count;
    }

    public async Task<long> CreateAttachmentImg(
        long attachmentId, FileItem file, string? description, CancellationToken ct)
    {
        logger.LogInformation("AttachmentImg creating start");

        if (!await attachmentRepository.Exists(attachmentId, ct))
        {
            logger.LogInformation("Attachment{AttachmentId} not found", attachmentId);
            throw new NotFoundException($"Attachment {attachmentId} not found");
        }

        string path;
        try
        {
            path = await fileService.UploadFile(file.Content, file.FileName, file.ContentType, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to upload file to storage");
            throw new Exception("File storage error");
        }

        var (attachmentImg, errors) = AttachmentImg.Create(
            0,
            attachmentId,
            path,
            description);

        if (errors is not null && errors.Any() || attachmentImg == null)
        {
            await fileService.DeleteFile(path, ct);

            var errorMsg = string.Join(", ", errors!);
            logger.LogError("attachmentImg validation failed: {Errors}", errorMsg);
            throw new ConflictException($"Validation failed: {errorMsg}");
        }

        var attachmentImgRes = await attachmentImgRepository.Create(attachmentImg, ct);

        logger.LogInformation("AttachmentImg creating success");

        return attachmentImgRes;
    }

    public async Task<long> UpdateAttachmentImg(
        long id, string? filePath, string? description, CancellationToken ct)
    {
        logger.LogInformation("AttachmentImg updating start");

        var attachmentImg = await attachmentImgRepository.Update(id, filePath, description, ct);

        logger.LogInformation("AttachmentImg updating success");

        return attachmentImg;
    }

    public async Task<long> DeleteAttachmentImg(long id, CancellationToken ct)
    {
        logger.LogInformation("AttachmentImg deleting start");

        var img = await attachmentImgRepository.GetById(id, ct);

        if (img is null)
        {
            logger.LogInformation("AttachmentImg{AcceptanceImgId} not found", id);
            throw new NotFoundException($"AttachmentImg {id} not found");
        }

        await fileService.DeleteFile(img.FilePath, ct);

        var attachmentImg = await attachmentImgRepository.Delete(id, ct);

        logger.LogInformation("AttachmentImg deleting start");

        return attachmentImg;
    }
}

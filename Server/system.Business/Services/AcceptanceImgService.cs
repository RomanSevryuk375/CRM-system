// Ignore Spelling: Img

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.AcceptanceImg;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class AcceptanceImgService(
    IAcceptanceImgRepository acceptanceImgRepository,
    IAcceptanceRepository acceptanceRepository,
    ILogger<AcceptanceImgService> logger,
    IFileService fileService) : IAcceptanceImgService
{
    public async Task<AcceptanceImgItem> GetAcceptanceImgById(long id, CancellationToken ct)
    {
        return await acceptanceImgRepository.GetById(id, ct)
               ?? throw new NotFoundException($"AcceptanceImg {id} not found");
    }
    
    public async Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting acceptanceImg start");

        var acceptanceImg = await acceptanceImgRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting acceptanceImg success");

        return acceptanceImg;
    }

    public async Task<(Stream fileStream, string contentType)> GetImageStream(long id, CancellationToken ct)
    {
        var img = await acceptanceImgRepository.GetById(id, ct)
            ?? throw new NotFoundException($"Image {id} not found");
        var stream = await fileService.GetFile(img.FilePath, ct);

        const string contentType = "application/octet-stream";

        return (stream, contentType);
    }

    public async Task<int> GetCountAcceptanceImg(AcceptanceImgFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count of acceptanceImg start");

        var count = await acceptanceImgRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count of acceptanceImg success");

        return count;
    }

    public async Task<long> CreateAcceptanceImg(
        long acceptanceId, FileItem file, string? description, CancellationToken ct)
    {
        logger.LogInformation("Creating acceptanceImg start");

        if (!await acceptanceRepository.Exists(acceptanceId, ct))
        {
            logger.LogError("Acceptance {AcceptanceId} not found", acceptanceId);
            throw new NotFoundException($"Acceptance {acceptanceId} not found");
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

        var (acceptanceImg, errors) = AcceptanceImg.Create(
            0,
            acceptanceId,
            path,
            description);

        if (errors is not null && errors.Any() || acceptanceImg == null)
        {
            await fileService.DeleteFile(path, ct);

            var errorMsg = string.Join(", ", errors!);
            logger.LogError("AttachmentImg validation failed: {Errors}", errorMsg);
            throw new ConflictException($"Validation failed: {errorMsg}");
        }

        var id = await acceptanceImgRepository.Create(acceptanceImg, ct);

        logger.LogInformation("Creating acceptanceImg success");

        return id;
    }

    public async Task<long> UpdateAcceptanceImg(
        long id, string? filePath, string? description, CancellationToken ct)
    {
        logger.LogInformation("Updating Acceptance{AcceptanceId} start", id);

        var acceptance = await acceptanceImgRepository.Update(id, filePath, description, ct);

        logger.LogInformation("Updating Acceptance{AcceptanceId} success", id);

        return acceptance;
    }

    public async Task<long> DeleteAcceptanceImg(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting Acceptance{AcceptanceId} start", id);

        var img = await acceptanceImgRepository.GetById(id, ct);

        if (img is null)
        {
            logger.LogInformation("AcceptanceImg{AcceptanceImgId} not found", id);
            throw new NotFoundException($"AcceptanceImg {id} not found");
        }

        await fileService.DeleteFile(img.FilePath, ct);

        var acceptanceId = await acceptanceImgRepository.Delete(id, ct);

        logger.LogInformation("Deleting Acceptance{AcceptanceId} success", id);

        return acceptanceId;
    }
}

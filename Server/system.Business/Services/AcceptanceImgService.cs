// Ignore Spelling: Img

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using CRMSystem.Core.ProjectionModels.AccetanceImg;

namespace CRMSystem.Business.Services;

public class AcceptanceImgService : IAcceptanceImgService
{
    private readonly IAcceptanceImgRepository _acceptanceImgRepository;
    private readonly IAcceptanceRepository _acceptanceRepository;
    private readonly ILogger<AcceptanceImgService> _logger;
    private readonly IFileService _fileService;

    public AcceptanceImgService(
        IAcceptanceImgRepository acceptanceImgRepository,
        IAcceptanceRepository acceptanceRepository,
        ILogger<AcceptanceImgService> logger,
        IFileService fileService)
    {
        _acceptanceImgRepository = acceptanceImgRepository;
        _acceptanceRepository = acceptanceRepository;
        _logger = logger;
        _fileService = fileService;
    }

    public async Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter)
    {
        _logger.LogInformation("Getting acceptanceImg start");

        var acceptanceImg = await _acceptanceImgRepository.GetPaged(filter);

        _logger.LogInformation("Getting acceptanceImg success");

        return acceptanceImg;
    }

    public async Task<(Stream FileStream, string ContentType)> GetImageStream(long id)
    {
        var img = await _acceptanceImgRepository.GetById(id)
            ?? throw new NotFoundException($"Image {id} not found");
        var stream = await _fileService.GetFile(img.FilePath);

        string contentType = "application/octet-stream";

        return (stream, contentType);
    }

    public async Task<int> GetCountAcceptanceImg(AcceptanceImgFilter filter)
    {
        _logger.LogInformation("Getting count of acceptanceImg start");

        var count = await _acceptanceImgRepository.GetCount(filter);

        _logger.LogInformation("Getting count of acceptanceImg success");

        return count;
    }

    public async Task<long> CreateAcceptanceImg(long AcceptanceId, FileItem file, string? description)
    {
        _logger.LogInformation("Creating acceptanceImg start");

        if (!await _acceptanceRepository.Exists(AcceptanceId))
        {
            _logger.LogError("Acceptance {AcceptanceId} not found", AcceptanceId);
            throw new NotFoundException($"Acceptance {AcceptanceId} not found");
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

        var (acceptanceImg, errors) = AcceptanceImg.Create(
            0,
            AcceptanceId,
            path,
            description);

        if (errors is not null && errors.Any() || acceptanceImg == null)
        {
            await _fileService.DeleteFile(path);

            var errorMsg = string.Join(", ", errors!);
            _logger.LogError("AttachmentImg validation failed: {Errors}", errorMsg);
            throw new ConflictException($"Validation failed: {errorMsg}");
        }

        var Id = await _acceptanceImgRepository.Create(acceptanceImg!);

        _logger.LogInformation("Creating acceptanceImg success");

        return Id;
    }

    public async Task<long> UpdateAcceptanceImg(long id, string? filePath, string? description)
    {
        _logger.LogInformation("Updating Acceptance{AcceptanceId} start", id);

        var acceptance = await _acceptanceImgRepository.Update(id, filePath, description);

        _logger.LogInformation("Updating Acceptance{AcceptanceId} success", id);

        return acceptance;
    }

    public async Task<long> DeleteAcceptanceImg(long id)
    {
        _logger.LogInformation("Deleting Acceptance{AcceptanceId} start", id);

        var img = await _acceptanceImgRepository.GetById(id);

        if (img is null)
        {
            _logger.LogInformation("AcceptanceImg{AcceptanceImgId} not found", id);
            throw new NotFoundException($"AcceptanceImg {id} not found");
        }

        await _fileService.DeleteFile(img.FilePath);

        var Id = await _acceptanceImgRepository.Delete(id);

        _logger.LogInformation("Deleting Acceptance{AcceptanceId} success", id);

        return Id;
    }
}

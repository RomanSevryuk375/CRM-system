using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.Core.DTOs.AcceptanceImg;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class AcceptanceImgService : IAcceptanceImgService
{
    private readonly IAcceptanceImgRepository _acceptanceImgRepository;
    private readonly IAcceptanceRepository _acceptanceRepository;
    private readonly ILogger<AcceptanceImgService> _logger;

    public AcceptanceImgService(
        IAcceptanceImgRepository acceptanceImgRepository,
        IAcceptanceRepository acceptanceRepository,
        ILogger<AcceptanceImgService> logger)
    {
        _acceptanceImgRepository = acceptanceImgRepository;
        _acceptanceRepository = acceptanceRepository;
        _logger = logger;
    }

    public async Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter)
    {
        _logger.LogInformation("Getting acceptanceImg start");

        var acceptance = await _acceptanceImgRepository.GetPaged(filter);

        _logger.LogInformation("Getting acceptanceImg success");

        return acceptance;
    }

    public async Task<int> GetCountAccptnceImg(AcceptanceImgFilter filter)
    {
        _logger.LogInformation("Getting count of acceptanceImg start");

        var count = await _acceptanceImgRepository.GetCount(filter);

        _logger.LogInformation("Getting count of acceptanceImg success");

        return count;
    }

    public async Task<long> CreateAccptanceImg(AcceptanceImg acceptanceImg)
    {
        _logger.LogInformation("Creating acceptanceImg start");

        var acceptanceFilter = new AcceptanceFilter
        (
            new[] { acceptanceImg.AcceptanceId },
            null,
            null,
            null,
            1,
            5,
            true
        );

        var acceptance = await _acceptanceRepository.GetPaged(acceptanceFilter); 

        if (!acceptance.Any())
        {
            _logger.LogError("Acceptance {AcceptanceId} not found", acceptanceImg.AcceptanceId);

            throw new Exception($"Acceptance {acceptanceImg.AcceptanceId} not found");
        }

        var accptanceImg = await _acceptanceImgRepository.Create(acceptanceImg);

        _logger.LogInformation("Creating acceptanceImg success");

        return accptanceImg;
    }

    public async Task<long> Update(long id, string? filePath, string? description)
    {
        _logger.LogInformation("Updating Acceptance{AcceptanceId} start", id);

        var acceptance = await _acceptanceImgRepository.Update(id, filePath, description);

        _logger.LogInformation("Updating Acceptance{AcceptanceId} success", id);

        return acceptance;
    }

    public async Task<long> Delete(long id)
    {
        _logger.LogInformation("Deleting Acceptance{AcceptanceId} start", id);

        var Id = await _acceptanceImgRepository.Delete(id);

        _logger.LogInformation("Deleting Acceptance{AcceptanceId} success", id);

        return Id;
    }
}

using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.Core.DTOs.AcceptanceImg;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class AcceptanceImgService : IAcceptanceImgService
{
    private readonly IAcceptanceImgRepository _acceptanceImgRepository;
    private readonly IAcceptanceRepository _acceptanceRepository;

    public AcceptanceImgService(
        IAcceptanceImgRepository acceptanceImgRepository,
        IAcceptanceRepository acceptanceRepository)
    {
        _acceptanceImgRepository = acceptanceImgRepository;
        _acceptanceRepository = acceptanceRepository;
    }

    public async Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter)
    {
        return await _acceptanceImgRepository.GetPaged(filter);
    }

    public async Task<int> GetCountAccptnceImg(AcceptanceImgFilter filter)
    {
        return await _acceptanceImgRepository.GetCount(filter);
    }

    public async Task<long> CreateAccptanceImg(AcceptanceImg acceptanceImg)
    {
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

        var accptance = await _acceptanceRepository.GetPaged(acceptanceFilter)
            ?? throw new Exception($"Worker {acceptanceImg.AcceptanceId} not found");

        var accptanceImg = await _acceptanceImgRepository.Create(acceptanceImg);

        return accptanceImg;
    }

    public async Task<long> Update(long id, string? filePath, string? description)
    {
        return await _acceptanceImgRepository.Update(id, filePath, description);
    }

    public async Task<long> Delete(long id)
    {
        return await _acceptanceImgRepository.Delete(id);
    }
}

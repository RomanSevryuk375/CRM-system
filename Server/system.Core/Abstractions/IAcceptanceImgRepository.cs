// Ignore Spelling: Img

using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.AccetanceImg;

namespace CRMSystem.Core.Abstractions;

public interface IAcceptanceImgRepository
{
    Task<long> Create(AcceptanceImg acceptanceImg, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(AcceptanceImgFilter filter, CancellationToken ct);
    Task<List<AcceptanceImgItem>> GetPaged(AcceptanceImgFilter filter, CancellationToken ct);
    Task<AcceptanceImgItem?> GetById(long id, CancellationToken ct);
    Task<long> Update(long id, string? filePath, string? description, CancellationToken ct);
}
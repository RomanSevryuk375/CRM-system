// Ignore Spelling: Img

using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.AccetanceImg;

namespace CRMSystem.Core.Abstractions;

public interface IAcceptanceImgRepository
{
    Task<long> Create(AcceptanceImg acceptanceImg);
    Task<long> Delete(long id);
    Task<int> GetCount(AcceptanceImgFilter filter);
    Task<List<AcceptanceImgItem>> GetPaged(AcceptanceImgFilter filter);
    Task<AcceptanceImgItem?> GetById(long id);
    Task<long> Update(long id, string? filePath, string? description);
}
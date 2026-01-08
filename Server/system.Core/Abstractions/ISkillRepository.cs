using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISkillRepository
{
    Task<int> Create(Skill skill, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<SkillItem>> Get(SkillFilter filter, CancellationToken ct);
    Task<int> GetCount(SkillFilter filter, CancellationToken ct);
    Task<int> Update(int id, SkillUpdateModel model, CancellationToken ct);
}
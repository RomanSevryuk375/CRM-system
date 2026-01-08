using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISkillService
{
    Task<int> CreateSkill(Skill skill, CancellationToken ct);
    Task<int> DeleteSkill(int id, CancellationToken ct);
    Task<List<SkillItem>> GetPagedSkills(SkillFilter filter, CancellationToken ct);
    Task<int> GetSkillsCount(SkillFilter filter, CancellationToken ct);
    Task<int> UpdateSkill(int id, SkillUpdateModel model, CancellationToken ct);
}
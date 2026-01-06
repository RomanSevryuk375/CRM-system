using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISkillService
{
    Task<int> CreateSkill(Skill skill);
    Task<int> DeleteSkill(int id);
    Task<List<SkillItem>> GetPagedSkills(SkillFilter filter);
    Task<int> GetSkillsCount(SkillFilter filter);
    Task<int> UpdateSkill(int id, SkillUpdateModel model);
}
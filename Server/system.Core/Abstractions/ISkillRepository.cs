using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISkillRepository
{
    Task<int> Create(Skill skill);
    Task<int> Delete(int id);
    Task<List<SkillItem>> Get(SkillFilter filter);
    Task<int> GetCount(SkillFilter filter);
    Task<int> Update(int id, SkillUpdateModel model);
}
using CRMSystem.Core.DTOs.Skill;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISkillRepository
    {
        Task<int> Create(Skill skill);
        Task<int> Delete(int id);
        Task<List<SkillItem>> Get(SkillFilter filter);
        Task<int> GetCount(SkillFilter filter);
        Task<int> Update(int id, SkillUpdateModel model);
    }
}
using AutoMapper;
using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class SkillProfile : Profile
{
    public SkillProfile()
    {
        CreateMap<SkillEntity, SkillItem>()
            .ForMember(dest => dest.Worker,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"))
            .ForMember(dest => dest.Specialization,
                        opt => opt.MapFrom(src => $"{src.Specialization!.Name}"));
    }
}

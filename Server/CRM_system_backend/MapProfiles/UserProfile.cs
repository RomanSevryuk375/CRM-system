using AutoMapper;
using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserItem>()
            .ForMember(dest => dest.Role,
                        opt => opt.MapFrom(src => $"{src.Role!.Name}"));
    }
}

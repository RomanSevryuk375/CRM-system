using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleItem, RoleResponse>();

        CreateMap<RoleEntity, RoleItem>();
    }
}

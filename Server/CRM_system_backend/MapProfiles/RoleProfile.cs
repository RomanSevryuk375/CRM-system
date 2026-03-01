using AutoMapper;
using CRMSystem.Core.ProjectionModels.Role;
using CRMSystem.DataAccess.Entities;
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

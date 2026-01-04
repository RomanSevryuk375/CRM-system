using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleItem, RoleResponse>();

        CreateMap<RoleEntity, RoleItem>();
    }
}

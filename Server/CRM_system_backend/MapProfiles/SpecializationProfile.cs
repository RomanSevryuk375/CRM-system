using AutoMapper;
using CRM_system_backend.Contracts.Specialization;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class SpecializationProfile : Profile
{
    public SpecializationProfile()
    {
        CreateMap<SpecializationItem, SpecializationResponse>();

        CreateMap<SpecializationEntity, SpecializationItem>();
    }
}

using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Specialization;

namespace CRM_system_backend.MapProfiles;

public class SpecializationProfile : Profile
{
    public SpecializationProfile()
    {
        CreateMap<SpecializationItem, SpecializationResponse>();

        CreateMap<SpecializationEntity, SpecializationItem>();
    }
}

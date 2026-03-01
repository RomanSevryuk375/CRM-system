using AutoMapper;
using CRMSystem.Core.ProjectionModels.Specialization;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts.Specialization;

namespace CRM_system_backend.MapProfiles;

public class SpecializationProfile : Profile
{
    public SpecializationProfile()
    {
        CreateMap<SpecializationItem, SpecializationResponse>();

        CreateMap<SpecializationEntity, SpecializationItem>();

        CreateMap<SpecializationRequest, SpecializationCreateModel>();
    }
}

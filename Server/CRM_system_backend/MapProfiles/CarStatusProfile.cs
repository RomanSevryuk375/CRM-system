using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class CarStatusProfile : Profile
{
    public CarStatusProfile()
    {
        CreateMap<CarStatusItem, CarStatusResponse>();

        CreateMap<CarStatusEntity, CarStatusItem>();
    }
}

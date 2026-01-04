using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class CarStatusProfile : Profile
{
    public CarStatusProfile()
    {
        CreateMap<CarStatusItem, CarStatusResponse>();

        CreateMap<CarStatusEntity, CarStatusItem>();
    }
}

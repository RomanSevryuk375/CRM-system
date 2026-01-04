using AutoMapper;
using CRMSystem.Core.DTOs.Car;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarItem, CarResponse>();

        CreateMap<CarEntity, CarItem>()
            .ForMember(dest => dest.Owner,
                        opt => opt.MapFrom(src => $"{src.Client!.Name} {src.Client.Surname}"))
            .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(src => $"{src.Status!.Name}"));
    }
}

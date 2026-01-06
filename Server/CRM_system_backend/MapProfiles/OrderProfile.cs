using AutoMapper;
using CRM_system_backend.Contracts.Order;
using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderItem, OrderResponse>();

        CreateMap<OrderEntity, OrderItem>()
            .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(src => $"{src.Status!.Name}"))
            .ForMember(dest => dest.Car,
                        opt => opt.MapFrom(src => $"{src.Car!.Brand} ({src.Car.StateNumber})"))
            .ForMember(dest => dest.Priority,
                        opt => opt.MapFrom(src => src.OrderPriority!.Name));
    }
}

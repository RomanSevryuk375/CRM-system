using AutoMapper;
using CRM_system_backend.Contracts.WorkInOrder;
using CRMSystem.Core.DTOs.WorkInOrder;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class WorkInOrderProfile : Profile
{
    public WorkInOrderProfile()
    {
        CreateMap<WorkInOrderItem, WorkInOrderResponse>();

        CreateMap<WorkInOrderEntity, WorkInOrderEntity>()
            .ForMember(dest => dest.Work,
                        opt => opt.MapFrom(src => $"{src.Work!.Title}"))
            .ForMember(dest => dest.Worker,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"))
            .ForMember(dest => dest.WorkInOrderStatus,
                        opt => opt.MapFrom(src => $"{src.WorkInOrderStatus!.Name}"));
    }
}

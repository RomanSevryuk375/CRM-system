using AutoMapper;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.WorkInOrder;

namespace CRM_system_backend.MapProfiles;

public class WorkInOrderProfile : Profile
{
    public WorkInOrderProfile()
    {
        CreateMap<WorkInOrderItem, WorkInOrderResponse>();

        CreateMap<WorkInOrderEntity, WorkInOrderItem>()
            .ForMember(dest => dest.Job,
                        opt => opt.MapFrom(src => $"{src.Work!.Title}"))
            .ForMember(dest => dest.Worker,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"))
            .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(src => $"{src.WorkInOrderStatus!.Name}"));
    }
}

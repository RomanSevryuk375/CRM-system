using AutoMapper;
using CRM_system_backend.Contracts.Schedule;
using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class ScheduleProfile : Profile
{
    public ScheduleProfile()
    {
        CreateMap<ScheduleItem, ScheduleResponse>();

        CreateMap<ScheduleEntity, ScheduleItem>()
            .ForMember(dest => dest.Worker,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"))
            .ForMember(dest => dest.Shift,
                        opt => opt.MapFrom(src => $"{src.Shift!.Name}"));
    }
}

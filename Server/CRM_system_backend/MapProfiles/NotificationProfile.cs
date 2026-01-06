using AutoMapper;
using CRM_system_backend.Contracts.Notification;
using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfile;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<NotificationItem, NotificationResponse>();

        CreateMap<NotificationEntity, NotificationItem>()
            .ForMember(dest => dest.Client,
                        opt => opt.MapFrom(src => $"{src.Client!.Name} {src.Client.Surname}"))
            .ForMember(dest => dest.Car,
                        opt => opt.MapFrom(src => $"{src.Car!.Brand} ({src.Car.StateNumber})"))
            .ForMember(dest => dest.Type,
                        opt => opt.MapFrom(src => $"{src.NotificationType!.Name}"))
            .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(src => $"{src.Status!.Name}"));
    }
}

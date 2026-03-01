using AutoMapper;
using CRMSystem.Core.ProjectionModels.NotificationType;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class NotificationTypeProfile : Profile
{
    public NotificationTypeProfile()
    {
        CreateMap<NotificationTypeItem, NotificationTypeResponse>();

        CreateMap<NotificationTypeEntity, NotificationTypeItem>();
    }
}

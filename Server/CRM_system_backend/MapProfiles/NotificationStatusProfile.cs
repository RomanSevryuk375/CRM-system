using AutoMapper;
using CRMSystem.Core.ProjectionModels.NotificationStatus;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class NotificationStatusProfile : Profile
{
    public NotificationStatusProfile()
    {
        CreateMap<NotificationStatusItem, NotificationStatusResponse>();

        CreateMap<NotificationStatusEntity, NotificationStatusItem>();
    }
}

using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class OrderPriorityProfile : Profile
{
    public OrderPriorityProfile()
    {
        CreateMap<OrderPriorityItem, OrderPriorityResponse>();

        CreateMap<OrderPriorityEntity, OrderPriorityItem>();
    }
}

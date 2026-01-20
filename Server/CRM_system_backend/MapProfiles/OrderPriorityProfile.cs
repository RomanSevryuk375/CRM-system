using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class OrderPriorityProfile : Profile
{
    public OrderPriorityProfile()
    {
        CreateMap<OrderPriorityItem, OrderPriorityResponse>();

        CreateMap<OrderPriorityEntity, OrderPriorityItem>();
    }
}
